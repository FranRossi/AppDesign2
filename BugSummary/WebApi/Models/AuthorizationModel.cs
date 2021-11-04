

using Domain.DomainUtilities;

namespace WebApi.Models
{
    public class AuthorizationModel
    {
        public string Token { get; set; }
        public RoleType Role { get; set; }
        
        
    }

}