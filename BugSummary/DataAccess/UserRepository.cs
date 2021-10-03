﻿using DataAccessInterface;
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
            User userFromDb = Context.Users.FirstOrDefault(u => u.UserName == username);
            if (userFromDb != null)
            {
                userFromDb.Token = token;
                Context.Users.Update(userFromDb);
            }
        }

        public RoleType GetRoleByToken(string token)
        {
            RoleType result = RoleType.Invalid;
            if (token != null)
            {
                User userFromDb = Context.Users.FirstOrDefault(u => u.Token == token);
                if (userFromDb != null)
                    result = userFromDb.Role;
            }
            return result;
        }

        public User Get(string token)
        {
            User result = null;
            if (token != null)
            {
                User userFromDb = Context.Users.FirstOrDefault(u => u.Token == token);
                if (userFromDb != null)
                    result = userFromDb;
            }
            return result;
        }
    }
}