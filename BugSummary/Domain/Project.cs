using Domain.DomainUtilities;
using Domain.DomainUtilities.CustomExceptions;
using System;
using System.Collections.Generic;

namespace Domain
{
    public class Project
    {
        public const int MaxProjectNameLength = 30;

        public int Id { get; set; }
        public string Name
        {
            get => _name;
            set
            {
                ValidateName(value);
                _name = value;
            }
        }
        public List<Bug> Bugs { get; set; }
        public List<User> Users { get; set; }

        private string _name;

        private void ValidateName(string nameToValidate)
        {
            if (!Validator.MaxLengthOfString(nameToValidate, MaxProjectNameLength))
                throw new ProjectNameLengthIncorrectException();
        }

        public void AddUser(User newUser)
        {
            if (newUser.Role == RoleType.Developer || newUser.Role == RoleType.Tester)
            {
                if (Users == null)
                    Users = new List<User>();
                Users.Add(newUser);
            }
            else
                throw new InvalidProjectAssigneeRole();
        }
    }
}