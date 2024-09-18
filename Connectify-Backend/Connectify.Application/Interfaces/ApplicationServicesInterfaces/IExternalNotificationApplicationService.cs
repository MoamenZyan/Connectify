using Connectify.Application.DTOs;
using Connectify.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connectify.Application.Interfaces.ApplicationServicesInterfaces
{
    public interface IExternalNotificationApplicationService
    {
        Task WelcomeEmailNotification(User receiver);
        Task ReceivedFriendRequestEmailNotification(UserDto sender, UserDto receiver);
        Task FriendRequestAcceptedNotification(UserDto sender, UserDto receiver);
    }
}
