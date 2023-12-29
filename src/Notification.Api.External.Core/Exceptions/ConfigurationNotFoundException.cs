namespace Notification.Api.External.Core.Exceptions;
public class ConfigurationNotFoundException : Exception
{
    public ConfigurationNotFoundException(string configurationKey) : base($"Configuration {configurationKey} not found") { }
}