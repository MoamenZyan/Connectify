using Connectify.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connectify.Domain.Entities
{
    public class Message
    {
        public Guid Id { get; set; }
        public Guid SenderId { get; set; }
        public Guid ChatId { get; set; }
        public string Content { get; set; } = null!;
        public string AttachmentPath { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public MessageStatus Status { get; set; }

        public virtual User Sender { get; set; } = null!;
        public virtual Chat Chat { get; set; } = null!;
        public virtual List<UserSeenMessage> MessageViewers { get; set; } = new List<UserSeenMessage>();
    }
}
