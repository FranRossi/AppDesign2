

using Domain.DomainUtilities;

namespace WebApi.Models
{
    public class AuthorizationModel
    {
        public string Token { get; set; }
        public RoleType Role { get; set; }


        public static AuthorizationModel ToModel(string token, RoleType role)
        {
            return new AuthorizationModel()
            {
                Token = token,
                Role = role
            };
        }
    }

}