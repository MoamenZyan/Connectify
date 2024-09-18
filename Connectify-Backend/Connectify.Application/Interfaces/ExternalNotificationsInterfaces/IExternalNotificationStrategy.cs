using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connectify.Application.Interfaces.ExternalNotificationsInterfaces
{
    public interface IExternalNotificationStrategy
    {
        Task Send(string userName, string to, Dictionary<string, string> data = null!);
    }
}
