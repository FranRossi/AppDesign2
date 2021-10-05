using Domain;
using Domain.DomainUtilities;

namespace WebApi.Models
{
    public class UserModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public RoleType Role { get; set; }

        public User ToEntity()
        {
            return new()
            {
                FirstName = FirstName,
                LastName = LastName,
                UserName = UserName,
                Password = Password,
                Email = Email,
                Role = Role
            };
        }

        public static UserModel ToModel(User user)
        {
            var model = new UserModel();
            model.FirstName = user.FirstName;
            model.LastName = user.LastName;
            model.Email = user.Email;
            model.UserName = user.UserName;
            model.Role = user.Role;

            return model;
        }
    }
}