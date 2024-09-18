using Connectify.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connectify.Domain.Entities
{
    public class UserChat
    {
        public Guid UserId { get; set; }
        public Guid ChatId { get; set; }
        public UserRole Role { get; set; }


        public virtual User User { get; set; } = null!;
        public virtual Chat Chat { get; set; } = null!;
    }
}
