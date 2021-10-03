using Domain;
using Domain.DomainUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterface
{
    public interface IUserRepository : IRepository<User>
    {
        public bool Authenticate(string username, string password);

        public void UpdateToken(string username, string token);

        public RoleType GetRoleByToken(string token);
        User Get(string token);
    }
}
