using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connectify.Domain.Entities
{
    public class UserBlocks
    {
        public Guid BlockerId { get; set; }
        public Guid BlockedId { get; set; }

        public virtual User BlockerUser { get; set; } = null!;
        public virtual User BlockedUser { get; set; } = null!;
    }
}
