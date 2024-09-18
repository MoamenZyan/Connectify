using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connectify.Application.Interfaces.UtilitesInterfaces
{
    public interface IJWTService
    {
        string GenerateToken(Guid userId, string Email, string Phone);
    }
}
