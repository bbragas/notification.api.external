using Bogus;
using Microsoft.Extensions.Options;
using Notification.Api.External.Webhook.Configurations;
using System.Net;

namespace Notification.Api.External.IntegrationTests.Configurations;

public class ChangeIpStartupFilter : IStartupFilter

{
    private readonly Faker _faker;
    private readonly bool _isValidIp;
    private readonly IPAddress _ipAddressRemote;

    public ChangeIpStartupFilter(IOptions<IPAddressWrap> ipAddressMock)
    {
        _faker = new();
        _isValidIp = ipAddressMock.Value.ValidIp;
        _ipAddressRemote = ipAddressMock.Value.IPAddress;
    }

    public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
    {
        return app =>
        {
            var ipRemote = (_isValidIp ? _ipAddressRemote : _faker.Internet.IpAddress()).ToString();

            var ipWhitelistSettings = app.ApplicationServices
                .CreateScope()
                .ServiceProvider
                .GetRequiredService<IOptions<IpWhitelistSettings>>()
                .Value;

            ipWhitelistSettings.SmsWhitelist.Add(ipRemote);
            ipWhitelistSettings.EmailWhitelist.Add(ipRemote);

            app.UseMiddleware<FakeRemoteIpAddressMiddleware>(_ipAddressRemote);
            next(app);
        };
    }
}
