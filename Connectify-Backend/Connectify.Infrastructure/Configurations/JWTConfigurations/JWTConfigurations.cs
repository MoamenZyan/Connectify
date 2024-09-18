using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connectify.Infrastructure.Configurations.JWTConfigurations
{
    public class JWTConfigurations
    {
        public string Issuer { get; set; } = null!;
        public string Audience { get; set; } = null!;
        public string SigningKey { get; set; } = null!;
    }
}
