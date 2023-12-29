namespace Notification.Api.External.Core.Exceptions;
public class EnvelopPublishingException : Exception
{
    public Guid EnvelopId { get; }

    public EnvelopPublishingException(Guid envelopId, string message) : base($"EnvelopId={envelopId}, Details={message}")
    {
        EnvelopId = envelopId;
    }
}