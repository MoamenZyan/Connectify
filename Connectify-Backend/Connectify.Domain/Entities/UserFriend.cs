using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connectify.Domain.Entities
{
    public class UserFriend
    {
        public Guid UserId1 { get; set; }
        public Guid UserId2 { get; set; }

        public User User1 { get; set; } = null!;
        public User User2 { get; set; } = null!;
    }
}
