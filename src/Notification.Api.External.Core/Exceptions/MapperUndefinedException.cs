using System.Runtime.Serialization;

namespace Notification.Api.External.Core.Exceptions;
[Serializable]
public class MapperUndefinedException : Exception
{
    public MapperUndefinedException()
    {
    }

    public MapperUndefinedException(string? message) : base(message)
    {
    }

    public MapperUndefinedException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected MapperUndefinedException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}