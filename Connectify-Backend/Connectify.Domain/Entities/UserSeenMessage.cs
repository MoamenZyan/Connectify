using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connectify.Domain.Entities
{
    public class UserSeenMessage
    {
        public Guid UserId { get; set; }
        public Guid MessageId { get; set; }
        public DateTime SeenAt { get; set; }


        public virtual User User { get; set; } = null!;
        public virtual Message Message { get; set; } = null!;
    }
}
