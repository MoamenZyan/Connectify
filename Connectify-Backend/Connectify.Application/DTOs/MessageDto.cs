using Connectify.Domain.Entities;
using Connectify.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connectify.Application.DTOs
{
    public class MessageDto
    {
        public Guid Id { get; set; }
        public Guid SenderId { get; set; }
        public Guid ChatId { get; set; }
        public string Content { get; set; } = null!;
        public string AttachmentPath { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public MessageStatus Status { get; set; }
        public string SenderFullName { get; set; } = null!;
        public string SenderPhoto { get; set; } = null!;
        public virtual List<UserSeenMessageDto> MessageViewers { get; set; } = new List<UserSeenMessageDto>();

        public MessageDto(Message message)
        {
            if (message == null)
                return;
            Id = message.Id;
            SenderId = message.SenderId;
            ChatId = message.ChatId;
            Content = message.Content;
            AttachmentPath = message.AttachmentPath;
            Status = message.Status;
            CreatedAt = message.CreatedAt;
            SenderFullName = $"{message.Sender.Fname} {message.Sender.Lname}";
            SenderPhoto = message.Sender.Photo;

            MessageViewers = message.MessageViewers.Select(x => new UserSeenMessageDto(x)).ToList();
        }
    }
}
