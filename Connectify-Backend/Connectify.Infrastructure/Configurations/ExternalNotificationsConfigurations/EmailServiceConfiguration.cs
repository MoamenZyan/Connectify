using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connectify.Infrastructure.Configurations.ExternalNotificationsConfigurations
{
    public class EmailServiceConfiguration
    {
        public string ApiKey { get; set; } = null!;
        public string FromEmail { get; set; } = null!;
    }
}
