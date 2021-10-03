using DataAccessInterface;
using Domain;
using Domain.DomainUtilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(BugSummaryContext bugSummaryContext)
        {
            Context = bugSummaryContext;
        }

        public override IEnumerable<User> GetAll()
        {
            return Context.Users.ToList();
        }

        public bool Authenticate(string username, string password)
        {
            return Context.Users.Any(u => u.UserName == username && u.Password == password);
        }

        public void UpdateToken(string username, string token)
        {
            User userFromDB = Context.Users.FirstOrDefault(u => u.UserName == username);
            if (userFromDB != null)
            {
                userFromDB.Token = token;
                Context.Users.Update(userFromDB);
            }
        }

        public RoleType GetRoleByToken(string token)
        {
            RoleType result = RoleType.Invalid;
            if (token != null)
            {
                User userFromDB = Context.Users.FirstOrDefault(u => u.Token == token);
                if (userFromDB != null)
                    result = userFromDB.Role;
            }
            return result;
        }

        public User Get(string token)
        {
            return new User
            {
                Id = 1,
                FirstName = "Pepe",
                LastName = "Perez",
                Password = "pepe1234",
                UserName = "pp",
                Email = "pepe@gmail.com",
                Role = RoleType.Tester,
            };
        }
    }
}