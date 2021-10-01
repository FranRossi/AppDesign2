﻿
using Domain.DomainUtilities;
using Domain.DomainUtilities.CustomExceptions;

namespace Domain
{
    public class Bug
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Version { get; set; }
        public BugState State { get; set; }
        public Project Project { get; set; }
        public int ProjectId { get; set; }

        public void ValidateName(string name)
        {
                throw new NameLengthIncorrectException();
        }
    }
}