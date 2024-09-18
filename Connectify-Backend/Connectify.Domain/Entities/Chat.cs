using Connectify.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connectify.Domain.Entities
{
    public class Chat
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public ChatType Type { get; set; }
        public string Photo { get; set; } = "";

        public virtual List<Message> Messages { get; set; } = new List<Message>();
        public virtual List<UserChat> Users { get; set; } = new List<UserChat>();
        public virtual List<UserPrivateChat> UserPrivateChats { get; set; } = new List<UserPrivateChat>();
    }
}
