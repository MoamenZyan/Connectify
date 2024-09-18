using Connectify.Application.DTOs;
using Connectify.Application.Interfaces.ApplicationServicesInterfaces;
using Connectify.Application.Interfaces.ExternalNotificationsInterfaces;
using Connectify.Application.Interfaces.ExternalNotificationsInterfaces.EmailStrategies;
using Connectify.Application.Interfaces.RepositoriesInterfaces;
using Connectify.Domain.Entities;
using Connectify.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connectify.Application.Services.NotificationService
{
    public class NotificationApplicationService : INotificationApplicationService
    {
        private readonly IInternalNotificationApplicationService _internalNotificationService;
        private readonly IExternalNotificationApplicationService _externalNotificationService;
        public NotificationApplicationService(IInternalNotificationApplicationService internalNotificationService,
                                            IExternalNotificationApplicationService externalNotificationService)
        {
            _internalNotificationService = internalNotificationService;
            _externalNotificationService = externalNotificationService;

        }

        public async Task FriendRequestAcceptedNotification(UserDto sender, UserDto receiver)
        {
            await Task.WhenAll(_externalNotificationService.FriendRequestAcceptedNotification(sender, receiver),
                    _internalNotificationService.FriendRequestAcceptedNotification(sender, receiver));
        }

        public async Task ReceivedFriendRequestNotification(UserDto sender, UserDto receiver)
        {
            await Task.WhenAll(_internalNotificationService.ReceivedFriendRequestNotification(sender, receiver),
                    _externalNotificationService.ReceivedFriendRequestEmailNotification(sender, receiver));
        }

        public async Task SendWelcomeNotification(User receiver)
        {
            await Task.WhenAll(_internalNotificationService.WelcomeNotification(receiver),
                                _externalNotificationService.WelcomeEmailNotification(receiver));
        }
    }
}
