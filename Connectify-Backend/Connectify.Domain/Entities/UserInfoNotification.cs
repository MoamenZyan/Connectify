using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connectify.Domain.Entities
{
    public class UserInfoNotification
    {
        public Guid UserId { get; set; }
        public Guid NotificationId { get; set; }

        public User User { get; set; } = null!;
        public InfoNotification Notification { get; set; } = null!;
    }
}
