using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using Notification.Api.External.Webhook.Configurations;
using Notification.Api.External.Webhook.Controllers.v1;
using System.Net;

namespace Notification.Api.External.Webhook.Filters;

public class IpWhitelistFilter : ActionFilterAttribute
{
    private readonly ILogger<IpWhitelistFilter> _logger;
    private readonly IpWhitelistSettings _ipWhitelistOptions;
    public IpWhitelistFilter(
        ILogger<IpWhitelistFilter> logger,
        IOptions<IpWhitelistSettings> applicationOptionsAccessor)
    {
        _logger = logger;
        _ipWhitelistOptions = applicationOptionsAccessor.Value;
    }
    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        var isSmsDeliveryRequest = filterContext.Controller.GetType().Name == nameof(InfobipWebhookController);
        var whitelistIps = isSmsDeliveryRequest ? _ipWhitelistOptions.SmsWhitelist : _ipWhitelistOptions.EmailWhitelist;

         ValidateRequestIpWhitelist(filterContext, whitelistIps);
    }

    private void ValidateRequestIpWhitelist(ActionExecutingContext filterContext, ICollection<string> whitelistIps)
    {
        var ipAddress = filterContext.HttpContext.Connection.RemoteIpAddress;
        if(ipAddress is null)
        {
            _logger.LogWarning("Remote IP address not exist");
            SetErrorInContext(filterContext, HttpStatusCode.BadRequest);
            return;
        }

        if (ipAddress.IsIPv4MappedToIPv6) ipAddress = ipAddress.MapToIPv4();

        var isIpWhitelisted = whitelistIps.Any(ip => IPAddress.Parse(ip).Equals(ipAddress));

        if (isIpWhitelisted) 
        {
            base.OnActionExecuting(filterContext);
            return;
        }

        _logger.LogWarning("Request from Remote IP address: {ipAddress} is forbidden.", ipAddress);
        SetErrorInContext(filterContext, HttpStatusCode.Forbidden);
    }

    private static void SetErrorInContext(ActionExecutingContext filterContext, HttpStatusCode httpErrorCode)
    {
        filterContext.HttpContext.Response.StatusCode = (int)httpErrorCode;
        filterContext.Result = new EmptyResult();
    }
}
