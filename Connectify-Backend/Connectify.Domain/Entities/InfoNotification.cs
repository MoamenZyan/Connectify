using Connectify.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connectify.Domain.Entities
{
    public class InfoNotification
    {
        public Guid Id { get; set; }
        public string Content { get; set; } = null!;
        public NotificationType Type { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual List<UserInfoNotification> Users { get; set; } = new List<UserInfoNotification>();
    }
}
