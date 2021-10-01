using Domain.DomainUtilities.CustomExceptions;
using System;
using System.Collections.Generic;

namespace Domain
{
    public class Project
    {
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


        public void ValidateName(string name)
        {
            if(name.Length > 30)
            throw new ProjectNameLengthIncorrectException();
        }
    }
}