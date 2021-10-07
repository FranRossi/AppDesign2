using Domain.DomainUtilities;

namespace BusinessLogicInterface
{
    public interface ISessionLogic
    {
        public string GenerateToken();
        public string Authenticate(string username, string pass);
        public RoleType GetRoleByToken(string token);
    }
}