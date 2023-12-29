using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.WebUtilities;
using MongoDB.Bson;
using Notification.Api.External.Core.Enums;
using Notification.Api.External.Core.Models;
using Notification.Api.External.Data.Context;
using Notification.Api.External.Webhook.Controllers.v1;

namespace Notification.Api.External.IntegrationTests.Configurations;

public class WebhookIntegrationTestsFixture
{
    public static string MakeValidDummyUrlRequestEmailWebhook()
    {
        var param = new Dictionary<string, string?>
        {
            { "Assunto", "Assunto+do+envio" },
            { "CampanhaID", "1234" },
            { "Data", DateTime.Now.ToString("u") },
            { "Descricao", "endereço+invalido" },
            { "Email", "email@email.com.br" },
            { "Tipo", EmailEventTypes.Leitura.ToString() }
        };

        var pathController = nameof(IAgenteSmtpWebhookController).Replace("Controller", string.Empty);

        return QueryHelpers.AddQueryString($"/v1/{pathController}", param);
    }

    public static async Task ClearDatabase<T>(WebApplicationFactory<T> factory) where T : class
    {
        var dbContext = factory.Services.GetService<IDbContext>()!;
        await dbContext.GetCollection<NotificationDelivery>().DeleteManyAsync(new BsonDocument());
    }
}
