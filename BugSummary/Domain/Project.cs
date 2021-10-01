﻿using Domain.DomainUtilities.CustomExceptions;
using System;
using System.Collections.Generic;

namespace Domain
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Bug> Bugs { get; set; }
        public List<User> Users { get; set; }

        public void ValidateName(string name)
        {
            throw new ProjectNameLengthIncorrectException();
        }
    }
}