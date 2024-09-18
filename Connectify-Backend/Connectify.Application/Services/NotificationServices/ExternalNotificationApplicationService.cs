using Connectify.Application.Interfaces.ApplicationServicesInterfaces;
using Connectify.Application.Interfaces.ExternalNotificationsInterfaces.EmailStrategies;
using Connectify.Application.Interfaces.ExternalNotificationsInterfaces;
using Connectify.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Connectify.Application.DTOs;

namespace Connectify.Application.Services.NotificationServices
{
    public class ExternalNotificationApplicationService : IExternalNotificationApplicationService
    {
        private readonly IWelcomeEmailStrategy _welcomeEmailStrategy;
        private readonly IReceivedFriendRequestEmailStrategy _receivedFriendRequestEmailStrategy;
        private readonly IFriendRequestAcceptedEmailStrategy _acceptedFriendRequestAcceptedEmailStrategy;
        private readonly IExternalNotificationContext _notificationContext;
        public ExternalNotificationApplicationService(IWelcomeEmailStrategy welcomeEmailStrategy,
                                                IReceivedFriendRequestEmailStrategy receivedFriendRequestEmailStrategy,
                                                IExternalNotificationContext notificationContext,
                                                IFriendRequestAcceptedEmailStrategy acceptedFriendRequestAcceptedEmailStrategy)
        {
            _welcomeEmailStrategy = welcomeEmailStrategy;
            _receivedFriendRequestEmailStrategy = receivedFriendRequestEmailStrategy;
            _acceptedFriendRequestAcceptedEmailStrategy = acceptedFriendRequestAcceptedEmailStrategy;
            _notificationContext = notificationContext;
        }

        public async Task FriendRequestAcceptedNotification(UserDto sender, UserDto receiver)
        {
            _notificationContext.SetStrategy(_acceptedFriendRequestAcceptedEmailStrategy);
            Dictionary<string, string> data = new()
            {
                {"SenderName", sender.FullName}
            };
            await _notificationContext.Send(sender.FullName, receiver.Email, data);
        }

        public async Task ReceivedFriendRequestEmailNotification(UserDto sender, UserDto receiver)
        {
            _notificationContext.SetStrategy(_receivedFriendRequestEmailStrategy);
            Dictionary<string, string> data = new Dictionary<string, string>()
            {
                {"SenderName", sender.FullName }
            };
            await _notificationContext.Send(receiver.FullName, receiver.Email, data);
        }

        public async Task WelcomeEmailNotification(User receiver)
        {
            _notificationContext.SetStrategy(_welcomeEmailStrategy);
            await _notificationContext.Send(receiver.Fname, receiver.Email, null!);
        }
    }
}
