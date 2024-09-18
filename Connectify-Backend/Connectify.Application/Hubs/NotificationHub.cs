using Connectify.Application.DTOs;
using Connectify.Application.Interfaces.HubInterfaces;
using Connectify.Application.Interfaces.RedisInterfaces;
using Connectify.Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Connectify.Application.Hubs
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class NotificationHub : Hub, INotificationHub
    {
        private readonly IRedis _redis;
        public NotificationHub(IRedis redis)
        {
            _redis = redis;
        }
        public async Task SendInfoNotificationToSpecificUser(IHubContext<NotificationHub> context, InfoNotification infoNotification, Guid receiverId)
        {
            await context.Clients.User(Convert.ToString(receiverId)!).SendAsync("InfoNotificationReceive", new InfoNotificationDto(infoNotification));
            await _redis.Delete($"user:{receiverId}");
        }

        public async Task SendAssociatedNotificationToSpecificUser(IHubContext<NotificationHub> context, AssociatedInfoNotificationDto associatedInfoNotificationDto, Guid receiverId)
        {
            await context.Clients.User(Convert.ToString(receiverId)!).SendAsync("AssociatedNotificationReceive", associatedInfoNotificationDto);
            await _redis.Delete($"user:{receiverId}");
        }


        public async Task SendNotificationToAll(InfoNotification infoNotification)
        {
            await Clients.All.SendAsync("InfoNotificationReceive", infoNotification);
        }
    }
}
