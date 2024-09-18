using Connectify.Domain.Entities;
using Connectify.Domain.Factories;
using Connectify.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connectify.Domain.Services
{
    public class NotificationService : INotificationService
    {
        public InfoNotification CreateInfoNotification(string content)
        {
            InfoNotification infoNotification = new InfoNotification()
            {
                Id = Guid.NewGuid(),
                Content = content,
                CreatedAt = DateTime.Now,
                Type = Enums.NotificationType.Info
            };
            return infoNotification;
        }

        public AssociatedInfoNotification CreateAssociatedUserNotification(Guid associatedUserId, string content)
        {
            AssociatedInfoNotification associatedInfoNotification = new AssociatedInfoNotification()
            {
                Id = Guid.NewGuid(),
                AssoicatedUserId = associatedUserId,
                Content = content,
                CreatedAt = DateTime.Now,
                Type = Enums.NotificationType.UserAssociatedInfo,
            };
            return associatedInfoNotification;
        }
    }
}
