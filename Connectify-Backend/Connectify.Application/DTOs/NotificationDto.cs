using Connectify.Domain.Entities;
using Connectify.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connectify.Application.DTOs
{
    public class AssociatedInfoNotificationDto
    {
        public Guid Id { get; set; }
        public string Content { get; set; } = null!;
        public NotificationType Type { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid RequestSenderId { get; set; }
        public string RequestSenderName { get; set; } = null!;
        public string RequestSenderPhoto { get; set; } = null!;
        public AssociatedInfoNotificationDto(Guid senderId, string senderName, string senderPhoto, AssociatedInfoNotification notification)
        {
            Id = notification.Id;
            Content = notification.Content;
            Type = notification.Type;
            CreatedAt = notification.CreatedAt;
            RequestSenderId = senderId;
            RequestSenderName = senderName;
            RequestSenderPhoto = senderPhoto;
        }
    }

    public class InfoNotificationDto
    {
        public Guid Id { get; set; }
        public string Content { get; set; } = null!;
        public NotificationType Type { get; set; }
        public DateTime CreatedAt { get; set; }

        public InfoNotificationDto(InfoNotification notification)
        {
            Id = notification.Id;
            Content = notification.Content;
            Type = notification.Type;
            CreatedAt = notification.CreatedAt;
        }
    }

}
