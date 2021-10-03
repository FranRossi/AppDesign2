using Domain.DomainUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicInterface
{
    public interface ISessionLogic
    {
        public string GenerateToken();
        public string Authenticate(string username, string pass);
        public RoleType GetRoleByToken(string token);
    }
}
