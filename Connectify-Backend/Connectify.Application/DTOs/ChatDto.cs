using Connectify.Domain.Entities;
using Connectify.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connectify.Application.DTOs
{
    public class ChatDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public ChatType Type { get; set; }
        public string Photo { get; set; } = "";
        public Guid OwnerId { get; set; }

        public virtual Dictionary<Guid, MessageDto> Messages { get; set; } = new Dictionary<Guid, MessageDto>();
        public virtual List<UserMinimalDto> Users { get; set; } = new List<UserMinimalDto>();

        public ChatDto(Chat chat, Guid currentUserId, Guid ownerId)
        {
            if (chat == null)
                return;
            Id = chat.Id;
            Name = chat.Name;
            Description = chat.Description;
            CreatedAt = chat.CreatedAt;
            Type = chat.Type;
            Photo = chat.Photo;
            OwnerId = ownerId;

            Messages = chat.Messages.Select(x => new MessageDto(x)).ToDictionary(x => x.Id, x => x);
            Users = chat.Users.Where(x => x.UserId != currentUserId).Select(x => new UserMinimalDto(x.User)).ToList();
        }
    }
}
