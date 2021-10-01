﻿using Domain.DomainUtilities;

namespace Domain
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public RoleType Role { get; set; }
        public Project Project { get; set; }
        public int ProjectId { get; set; }
    }
}