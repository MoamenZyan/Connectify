using Connectify.Application.DTOs;
using Connectify.Application.Hubs;
using Connectify.Application.Interfaces.ApplicationServicesInterfaces;
using Connectify.Application.Interfaces.HubInterfaces;
using Connectify.Application.Interfaces.RepositoriesInterfaces;
using Connectify.Domain.Entities;
using Connectify.Domain.Factories;
using Connectify.Domain.Interfaces;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connectify.Application.Services.NotificationServices
{
    public class InternalNotificationApplicationService : IInternalNotificationApplicationService
    {
        private readonly INotificationService _notificationService;
        private readonly INotificationRepository<InfoNotification> _infoNotificationRepository;
        private readonly INotificationRepository<AssociatedInfoNotification> _associateInfoNotificationRepository;
        private readonly IUserNotificationRepository<UserInfoNotification> _userInfoNotificationRepository;
        private readonly IUserNotificationRepository<UserAssociatedInfoNotification> _userAssociatedInfoNotificationRepository;
        private readonly INotificationHub _notificationHub;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHubContext<NotificationHub> _hubContext;
        public InternalNotificationApplicationService(INotificationService notificationService,
                                                INotificationRepository<InfoNotification> infoNotificationRepository,
                                                INotificationRepository<AssociatedInfoNotification> associateInfoNotificationRepository,
                                                IUserNotificationRepository<UserInfoNotification> userInfoNotificationRepository,
                                                IUserNotificationRepository<UserAssociatedInfoNotification> userAssociatedInfoNotificationRepository,
                                                INotificationHub notificationHub,
                                                IUnitOfWork unitOfWork,
                                                IHubContext<NotificationHub> hubContext)
        {
            _notificationService = notificationService;
            _infoNotificationRepository = infoNotificationRepository;
            _associateInfoNotificationRepository = associateInfoNotificationRepository;
            _userAssociatedInfoNotificationRepository = userAssociatedInfoNotificationRepository;
            _userInfoNotificationRepository = userInfoNotificationRepository;
            _notificationHub = notificationHub;
            _unitOfWork = unitOfWork;
            _hubContext = hubContext;
        }

        public async Task FriendRequestAcceptedNotification(UserDto sender, UserDto receiver)
        {
            var notification = _notificationService.CreateAssociatedUserNotification(sender.Id, $"{sender.FullName}, Has accepted your friend request!");
            var userNotification = new UserAssociatedInfoNotification()
            {
                UserId = receiver.Id,
                NotificationId = notification.Id
            };

            notification.Type = Domain.Enums.NotificationType.UserAssociatedInfo;
            await _associateInfoNotificationRepository.AddAsync(notification);
            await _userAssociatedInfoNotificationRepository.AddAsync(userNotification);

            await Task.WhenAll(_unitOfWork.SaveChangesAsync(),
                        _notificationHub.SendAssociatedNotificationToSpecificUser(_hubContext, new DTOs.AssociatedInfoNotificationDto(sender.Id, sender.FullName, sender.Photo, notification), receiver.Id));
        }

        public async Task ReceivedFriendRequestNotification(UserDto sender, UserDto receiver)
        {
            var notification = _notificationService.CreateAssociatedUserNotification(sender.Id, $"You got friend request from {sender.FullName}");
            var userNotification = new UserAssociatedInfoNotification()
            {
                UserId = receiver.Id,
                NotificationId = notification.Id,
            };
            notification.Type = Domain.Enums.NotificationType.FriendRequest;
            await _associateInfoNotificationRepository.AddAsync(notification);
            await _userAssociatedInfoNotificationRepository.AddAsync(userNotification);

            await Task.WhenAll(_unitOfWork.SaveChangesAsync(),
                _notificationHub.SendAssociatedNotificationToSpecificUser(_hubContext, new DTOs.AssociatedInfoNotificationDto(sender.Id, sender.FullName, sender.Photo, notification), receiver.Id));
        }

        public async Task WelcomeNotification(User receiver)
        {
            var notification = _notificationService.CreateInfoNotification($"{receiver.Fname}, Welcome on board!");
            var userNotification = UserNotificationFactory.CreateUserInfoNotification(notification.Id, receiver.Id);

            await _infoNotificationRepository.AddAsync(notification);
            await _userInfoNotificationRepository.AddAsync(userNotification);

            await Task.WhenAll(_notificationHub.SendInfoNotificationToSpecificUser(_hubContext, notification, receiver.Id),
                            _unitOfWork.SaveChangesAsync());
        }
    }
}
