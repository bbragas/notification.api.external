using Bogus;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Moq;
using Notification.Api.External.Core.Contracts;
using Notification.Api.External.Core.Exceptions;
using Notification.Api.External.Webhook.Errors;
using System.Net;
using Notification.Api.External.IntegrationTests.Configurations;
using MediatR;
using Notification.Api.External.Eventbus.Publishers;

namespace Notification.Api.External.IntegrationTests.Webhook.Controllers;

[Collection("Sequential")]
public class IAgenteSmptpWebhookControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private HttpClient _client;
    private readonly Faker _faker;
    private readonly Mock<IMediator> _mediatorMock;
    private readonly WebApplicationFactory<Program> _factory;
    private readonly Mock<IEnvelopPublisher> _apiEventBusRepositoryMock;

    public IAgenteSmptpWebhookControllerTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _faker = new Faker();
        _mediatorMock = new Mock<IMediator>();
        _apiEventBusRepositoryMock = new Mock<IEnvelopPublisher>();

        _client = _factory.WithWebHostBuilder(builder =>
            builder.ConfigureTestServices(services =>
                services
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
                services
                    .AddTransient(_ => _mediatorMock.Object)
                    .ConfigureStartupFilter(new(true)))
            ).CreateClient();

        HttpResponseMessage response = await _client.GetAsync(WebhookIntegrationTestsFixture.MakeValidDummyUrlRequestEmailWebhook());
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Should_Return_Forbiden_If_Ip_Is_Not_In_Whitelist()
    {
        _apiEventBusRepositoryMock.SetupApiEventBusRepositoryPublishAsyncWithSuccess();

        _client = _factory.WithWebHostBuilder(builder =>
            builder.ConfigureTestServices(services =>
                services.ConfigureStartupFilter(new(false)))
        ).CreateClient();

        HttpResponseMessage response = await _client.GetAsync(WebhookIntegrationTestsFixture.MakeValidDummyUrlRequestEmailWebhook());
        Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
    }

    [Fact]
    public async Task Should_Return_InternalServerError_If_EnvelopPublishingException_Is_Thrown()
    {
        EnvelopPublishingException exceptionToThrow = new(_faker.Random.Guid(), _faker.Lorem.Sentence(4));

        _apiEventBusRepositoryMock
            .Setup(x => x.PublishAsync(It.IsAny<IEnvelope>(), It.IsAny<CancellationToken>()))
            .Throws(exceptionToThrow);

        _client = _factory.WithWebHostBuilder(builder =>
            builder.ConfigureTestServices(services =>
                services
                    .AddTransient(_ => _apiEventBusRepositoryMock.Object)
                    .ConfigureStartupFilter(new(true))
                    .AddDbContextInMemory())
            ).CreateClient();

        HttpResponseMessage response = await _client.GetAsync(WebhookIntegrationTestsFixture.MakeValidDummyUrlRequestEmailWebhook());

        var result = (await response.Content.ReadFromJsonAsync<ErrorDetail>())!;

        Assert.Equal(exceptionToThrow.Message, result.Message);
        Assert.Equal((int)HttpStatusCode.InternalServerError, result.StatusCode);
    }

    [Fact]
    public async Task Should_Return_Success_When_Envelope_Is_Send()
        => (await _client
            .GetAsync(WebhookIntegrationTestsFixture.MakeValidDummyUrlRequestEmailWebhook()))
            .EnsureSuccessStatusCode();
}
