using Bogus;
using System.Net;

namespace Notification.Api.External.IntegrationTests.Configurations;

public class IPAddressWrap
{
    private readonly Faker _faker = new Faker();

    public IPAddressWrap(bool validIp)
    {
        ValidIp = validIp;
        IPAddress = _faker.Internet.IpAddress();
    }

    public bool ValidIp { get; private set; }
    public IPAddress IPAddress { get; private set; }
}