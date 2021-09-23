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

        public UserRepository(DbContext context)
        {
            this._context = context;

        }

        public List<User> GetAll()
        {
            List<User> users = new List<User>
            {
                new User
                {
                    id = 1,
                    firstName = "Pepe",
                    lastName = "Perez",
                    password = "pepe1234",
                    userName = "pp",
                    email = "pepe@gmail.com",
                    role = RoleType.Admin
                }
            };
            return users;
        }
    }
}