namespace Notification.Api.External.Webhook.Models.v1.ExampleControllerModel;

/// <summary>
/// This is a example get response
/// </summary>
public class ExampleGetResponse
{

    public ExampleGetResponse(int id)
    {
        Id = id;
    }

    /// <summary>
    /// This is my property number 1
    /// </summary>
    public int Id { get; init; }
}
