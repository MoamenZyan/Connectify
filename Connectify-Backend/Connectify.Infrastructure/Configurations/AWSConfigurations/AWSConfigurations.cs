using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connectify.Infrastructure.Configurations.AWSConfigurations
{
    public class AWSConfigurations
    {
        public string AccessKey { get; set; } = null!;
        public string SecretAccessKey { get; set; } = null!;
        public string Region { get; set; } = null!;
        public string BucketName { get; set; } = null!;
    }
}
