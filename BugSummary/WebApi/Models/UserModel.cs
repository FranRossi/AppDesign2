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
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public RoleType Role { get; set; }

        public User ToEntity() => new User()
        {
            Id = this.Id,
            FirstName = this.FirstName,
            LastName = this.LastName,
            UserName = this.UserName,
            Password = this.Password,
            Email = this.Email,
            Role = this.Role
        };

    }
}
