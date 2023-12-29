using Microsoft.AspNetCore.Mvc;
using Notification.Api.External.Webhook.Models.v1.ExampleControllerModel;

namespace Notification.Api.External.Webhook.Controllers.v1;

[ApiVersion("1.0")]
[ApiController]
[Route("v{version:apiVersion}/[controller]")]
public class ExampleController : ControllerBase
{

    /// <summary>
    /// This is a example controller
    /// </summary>
    /// <remarks>
    /// If you are seeing it so please, don't give me alone, document the other endpoints :)
    /// </remarks>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(ExampleGetResponse),200)]
    public ExampleGetResponse Get() => new ExampleGetResponse(200);
}
