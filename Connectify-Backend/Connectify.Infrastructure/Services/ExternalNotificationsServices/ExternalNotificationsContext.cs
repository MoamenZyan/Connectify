using Connectify.Application.Interfaces.ExternalNotificationsInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connectify.Infrastructure.Services.ExternalNotificationsServices
{
    public class ExternalNotificationContext : IExternalNotificationContext
    {
        private IExternalNotificationStrategy _externalNotificationStrategy = null!;
        public void SetStrategy(IExternalNotificationStrategy strategy)
        {
            _externalNotificationStrategy = strategy;
        }

        public async Task Send(string userName, string to, Dictionary<string, string> data)
        {
            await _externalNotificationStrategy.Send(userName, to, data);
        }
    }
}
