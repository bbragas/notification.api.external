﻿using Notification.Api.External.Application.Events;
using Notification.Api.External.Application.Events.v1;
using Notification.Api.External.Core.Enums;

namespace Notification.Api.External.Application.MapperResolvers.Resolvers;

public class NotificationToEmailClickEventResolver : INotificationToEventResolver
{
    public bool ApplyTo(NotificationEvent reservation) =>
        reservation.EventType == EmailEventTypes.Clique.ToString();
    public IEvent Map(NotificationEvent source)=> new EmailClickEvent(source);
}