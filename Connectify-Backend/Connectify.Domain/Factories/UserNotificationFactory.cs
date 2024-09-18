using Connectify.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connectify.Domain.Factories
{
    public class UserNotificationFactory
    {
        public static UserInfoNotification CreateUserInfoNotification(Guid notificationId, Guid userId)
        {
            UserInfoNotification userInfoNotification = new UserInfoNotification()
            {
                NotificationId = notificationId,
                UserId = userId
            };
            return userInfoNotification;
        }

        public static UserAssociatedInfoNotification CreateUserAssociatedInfoNotification(Guid notificationId, Guid userId)
        {
            UserAssociatedInfoNotification userAssociatedInfoNotification = new UserAssociatedInfoNotification()
            {
                NotificationId = notificationId,
                UserId = userId,
            };

            return userAssociatedInfoNotification;
        }
    }
}
