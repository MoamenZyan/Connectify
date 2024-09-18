using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connectify.Domain.Entities
{
    public class UserPrivateChat
    {
        public Guid User1Id { get; set; }
        public Guid User2Id { get; set; }
        public Guid ChatId { get; set; }

        public User User1 { get; set; } = null!;
        public User User2 { get; set; } = null!;
        public Chat Chat { get; set; } = null!;
    }
}
