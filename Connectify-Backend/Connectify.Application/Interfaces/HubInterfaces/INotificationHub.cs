using Connectify.Application.DTOs;
using Connectify.Application.Hubs;
using Connectify.Domain.Entities;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connectify.Application.Interfaces.HubInterfaces
{
    public interface INotificationHub
    {
        Task SendNotificationToAll(InfoNotification infoNotification);
        Task SendInfoNotificationToSpecificUser(IHubContext<NotificationHub> context, InfoNotification infoNotification, Guid receiverId);
        Task SendAssociatedNotificationToSpecificUser(IHubContext<NotificationHub> context, AssociatedInfoNotificationDto associatedInfoNotificationDto, Guid receiverId);
    }
}
