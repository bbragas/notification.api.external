using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Notification.Api.External.Application.Events;
using Notification.Api.External.Application.MapperResolvers;
using Notification.Api.External.Core.Exceptions;
using Notification.Api.External.Core.Models;
using Notification.Api.External.Data.Repositories;
using Notification.Api.External.Eventbus.Envelopes;
using Notification.Api.External.Eventbus.Publishers;

namespace Notification.Api.External.Application.EventHandlers;
public class NotificationEventHandler : INotificationHandler<NotificationEvent>
{
    private readonly IMapper _mapper;
    private readonly IWriteRepository _writeRepository;
    private readonly IEnvelopPublisher _apiEventBusPublisher;
    private readonly ILogger<NotificationEventHandler> _logger;
    private readonly IMapperResolver<NotificationEvent, IEvent> _mapperResolver;

    public NotificationEventHandler(
        IMapper mapper,
        IWriteRepository writeRepository,
        IEnvelopPublisher apiEventBusPublisher,
        ILogger<NotificationEventHandler> logger,
        IMapperResolver<NotificationEvent, IEvent> mapperResolver)
    {
        _logger = logger;
        _mapper = mapper;
        _mapperResolver = mapperResolver;
        _writeRepository = writeRepository;
        _apiEventBusPublisher = apiEventBusPublisher;
    }

    public async Task Handle(NotificationEvent @event, CancellationToken cancellationToken)
    {
        var specificEvent = _mapperResolver.Map(@event);
        var envelope = _mapper.Map<Envelope>(specificEvent);
        var notificationData = _mapper.Map<NotificationDelivery>(@event);

        await _writeRepository.CreateAsync(notificationData, cancellationToken);
        var responseMessage = await _apiEventBusPublisher.PublishAsync(envelope, cancellationToken);

        if (responseMessage.IsSuccessStatusCode) return;

        _logger.LogError(
           "It was not possible to publish the envelop. Envelop={@Envelop}, PublishingResult={@PublishingResult}",
            envelope,
            responseMessage);

        throw new EnvelopPublishingException(envelope.Id, "It was not possible to publish the envelop.");
    }
}
