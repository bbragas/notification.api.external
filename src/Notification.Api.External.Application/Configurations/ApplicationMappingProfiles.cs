using AutoMapper;
using Notification.Api.External.Application.Events;
using Notification.Api.External.Eventbus.Envelopes;
using System.Text.Json;

namespace Notification.Api.External.Application.Configurations;
public class ApplicationMappingProfiles : Profile
{
    private static readonly JsonSerializerOptions _options = new()
    {
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public ApplicationMappingProfiles()
    {
        CreateMap<NotificationEvent, Core.Models.NotificationDelivery>()
            .ForMember(x => x.Id, opt => opt.Ignore())
            .ForMember(x => x.CreatedAt, opt => opt.Ignore());

        CreateMap<NotificationEventBase, Envelope>()
            .ConstructUsing(x => 
                new(x.Id, x.Version, x.Type, x.Source, x.Description, x.ContentType, x.ToBase64Data(_options), DateTime.UtcNow, x.Schema))
            .ForMember(x => x.Time, opt => opt.Ignore())
            .ForMember(x => x.Data, opt => opt.MapFrom(src => src.ToBase64Data(_options)))
            .ForMember(x => x.DataSchema, opt => opt.MapFrom(src => src.Schema))
            .ForMember(x => x.DataContentType, opt => opt.MapFrom(src => src.ContentType))
            .ForMember(x => x.SpecVersion, opt => opt.MapFrom(src => src.Version))
            .ForMember(x => x.Subject, opt => opt.MapFrom(src => src.Description));
    }
}
