﻿using CustomExceptions;
using Domain;

namespace WebApi.Models
{
    public class ProjectAddModel
    {
        public string Name { get; set; }

        public Project ToEntity()
        {
            if (Name == null)
                throw new ProjectModelMissingFieldException();
            return new()
            {
                Name = Name
            };
        }
    }
}