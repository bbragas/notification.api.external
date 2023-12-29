using System.Net;

namespace Notification.Api.External.IntegrationTests.Configurations;

public class FakeRemoteIpAddressMiddleware
{
    private readonly RequestDelegate next;
    private readonly IPAddress _fakeIpAddress;

    public FakeRemoteIpAddressMiddleware(RequestDelegate _next, IPAddress ipAddressMock)
    {
        next = _next;
        _fakeIpAddress = ipAddressMock;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        httpContext.Connection.RemoteIpAddress = _fakeIpAddress;

        await next(httpContext);
    }
}
