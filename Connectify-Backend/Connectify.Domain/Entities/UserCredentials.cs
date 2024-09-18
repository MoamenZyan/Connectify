using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connectify.Domain.Entities
{
    public class UserCredentials
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
