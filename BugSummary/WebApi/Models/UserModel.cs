using Domain;
using Domain.DomainUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public User ToEntity() => new User()
        {
            FirstName = this.FirstName,
            LastName = this.LastName,
            UserName = this.UserName,
            Password = this.Password,
            Email = this.Email,
            Role = this.Role
        };

        public static UserModel ToModel(User user)
        {
            UserModel model = new UserModel();
            model.FirstName = user.FirstName;
            model.LastName = user.LastName;
            model.Email = user.Email;
            model.UserName = user.UserName;
            model.Role = user.Role;

            return model;
        }
    }
}
