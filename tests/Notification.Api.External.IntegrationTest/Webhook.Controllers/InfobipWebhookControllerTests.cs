using Bogus;
using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Moq;
using Notification.Api.External.Core.Contracts;
using Notification.Api.External.Core.Enums;
using Notification.Api.External.Core.Exceptions;
using Notification.Api.External.Eventbus.Publishers;
using Notification.Api.External.IntegrationTests.Configurations;
using Notification.Api.External.Webhook.Controllers.v1;
using Notification.Api.External.Webhook.Dtos;
using Notification.Api.External.Webhook.Errors;
using System.Net;

namespace Notification.Api.External.IntegrationTests.Webhook.Controllers;

[Collection("Sequential")]
public class InfobipWebhookControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private HttpClient _client;
    private readonly Faker _faker;
    private readonly string _pathEndpoint;
    private readonly Mock<IMediator> _mediatorMock;
    private readonly WebApplicationFactory<Program> _factory;
    private readonly Mock<IEnvelopPublisher> _apiEventBusRepositoryMock;

    public InfobipWebhookControllerTests(WebApplicationFactory<Program> factory)
    {
        _faker = new();
        _factory = factory;
        _mediatorMock = new Mock<IMediator>();
        _apiEventBusRepositoryMock = new Mock<IEnvelopPublisher>();
        _pathEndpoint = nameof(InfobipWebhookController).Replace("Controller", "");

        _client = _factory.WithWebHostBuilder(builder =>
            builder.ConfigureTestServices(services =>
                services
                    .AddTransient(_ => _apiEventBusRepositoryMock.Object)
                    .ConfigureStartupFilter(new(true))
                    .AddDbContextInMemory())
            ).CreateClient();
    }

    [Fact]
    public async Task Should_Return_Ok_If_Ip_Is_In_Whitelist()
    {
        _mediatorMock.SetupMediatorPublishWithSuccess();
        _apiEventBusRepositoryMock.SetupApiEventBusRepositoryPublishAsyncWithSuccess();

        _client = _factory.WithWebHostBuilder(builder =>
            builder.ConfigureTestServices(services =>
            {
                services.AddTransient(_ => _mediatorMock.Object);
                services.ConfigureStartupFilter(new(true));
            })
        ).CreateClient();

        HttpResponseMessage response = await _client.GetAsync(WebhookIntegrationTestsFixture.MakeValidDummyUrlRequestEmailWebhook());
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Should_Return_Forbidden_If_Ip_Is_Not_In_Whitelist()
    {
        _apiEventBusRepositoryMock.SetupApiEventBusRepositoryPublishAsyncWithSuccess();

        _client = _factory.WithWebHostBuilder(builder =>
            builder.ConfigureTestServices(services => services.ConfigureStartupFilter(new(false)))
        ).CreateClient();

        HttpResponseMessage response = await _client.GetAsync(WebhookIntegrationTestsFixture.MakeValidDummyUrlRequestEmailWebhook());
        Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
    }

    [Fact()]
    public async Task Should_Return_Success_When_Envelope_Is_Send()
    {
        _apiEventBusRepositoryMock
            .Setup(x => x.PublishAsync(It.IsAny<IEnvelope>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK
            });

        (await _client.PostAsJsonAsync($"v1/{_pathEndpoint}/258", CreateSmsDeliveryResultDtoDummy())).EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task Should_Return_InternalServerError_If_EnvelopPublishingException_Is_Thrown()
    {
        EnvelopPublishingException exceptionToThrow = new(_faker.Random.Guid(), _faker.Lorem.Sentence(4));

        _apiEventBusRepositoryMock
            .Setup(x => x.PublishAsync(It.IsAny<IEnvelope>(), It.IsAny<CancellationToken>()))
            .Throws(exceptionToThrow);

        var _pathEndpoint = nameof(InfobipWebhookController).Replace("Controller", "");
        HttpResponseMessage response = await _client.PostAsJsonAsync($"v1/{_pathEndpoint}/258", CreateSmsDeliveryResultDtoDummy());

        var result = (await response.Content.ReadFromJsonAsync<ErrorDetail>())!;

        Assert.Equal(exceptionToThrow.Message, result.Message);
        Assert.Equal((int)HttpStatusCode.InternalServerError, result.StatusCode);
    }

    private SmsDeliveryDto CreateSmsDeliveryResultDtoDummy()
        => new()
        {
            Results = new()
            {
                new() {
                    Status = new()
                    {
                        Id = 18,
                        Name = "REJECTED_DESTINATION_NOT_REGISTERED",
                        GroupId= 5,
                        GroupName= SmsEventTypes.Rejected,
                        Description = "Destination not registered"
                    },
                    Error= new()
                    {
                        Id = 579,
                        Name= "EC_DEST_ADDRESS_NOT_IN_SMS_DEMO",
                        Description= "Destination address not in whitelist for SMS demo",
                        GroupId = 3,
                        GroupName  = "OPERATOR_ERRORS",
                        Permanent = true
                    },
                    DoneAt= DateTime.UtcNow,
                    MessageId= Guid.NewGuid().ToString(),
                    SentAt= DateTime.UtcNow,
                    To = _faker.Person.Phone,
                }
            }
        };
}