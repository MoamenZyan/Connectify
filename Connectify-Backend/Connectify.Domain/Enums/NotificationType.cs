using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connectify.Domain.Enums
{
    public enum NotificationType
    {
        Info, // Normal info notification.
        FriendRequest, // Friend request notification.
        UserAssociatedInfo // User associated notification (i.e Ahmed messaged you).
    }
}
