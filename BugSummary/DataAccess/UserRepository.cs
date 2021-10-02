using DataAccessInterface;
using Domain;
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
    }
}