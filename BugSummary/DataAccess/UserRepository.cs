using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Utilities;

namespace DataAccess
{
    public class UserRepository
    {
        private readonly DbContext _context;
        private readonly DbSet<User> _users;

        public UserRepository(BugSummaryContext context)
        {
            this._context = context;
            context.Set<User>();
            this._users = context.Users;
        }

        public IEnumerable<User> GetAll()
        {
            return this._users;
        }

        public void Create(User newUser)
        {
            this._users.Add(newUser);
            this._context.SaveChanges();
        }
    }
}