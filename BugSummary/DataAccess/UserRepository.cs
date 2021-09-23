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

        public UserRepository(DbContext context)
        {
            this._context = context;
            this._users = context.Set<User>();
        }

        public IEnumerable<User> GetAll()
        {
            return this._users;
        }
    }
}