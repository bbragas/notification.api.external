using Notification.Api.External.Core.Enums;

namespace Notification.Api.External.Webhook.Dtos;

public class SmsDeliveryDto
{
    public List<SmsReportDto> Results { get; set; } = default!;
}

public class SmsReportDto
{
    public StatusDto Status { get; set; } = default!;

    public ErrorDto Error { get; set; } = default!;

    public string MessageId { get; set; } = default!;

    public DateTime DoneAt { get; set; }

    public DateTime SentAt { get; set; }

    public string To { get; set; } = default!;
}

public class ErrorDto
{
    public int GroupId { get; set; }

    public string GroupName { get; set; } = default!;

    public int Id { get; set; }

    public string Name { get; set; } = default!;

    public string Description { get; set; } = default!;

    public bool Permanent { get; set; }
}

public class StatusDto
{
    public int GroupId { get; set; }

    public SmsEventTypes GroupName { get; set; }

    public int Id { get; set; }

    public string Name { get; set; } = default!;

    public string Description { get; set; } = default!;
}