using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connectify.Domain.Entities
{
    public class OTP
    {
        public Guid Id { get; set; }
        public string Number { get; set; } = null!;
        public DateTime Deadline { get; set; }
        public Guid UserId { get; set; }
    }
}
