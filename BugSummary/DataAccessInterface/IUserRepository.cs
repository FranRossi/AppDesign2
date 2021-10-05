using Domain;
using Domain.DomainUtilities;

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