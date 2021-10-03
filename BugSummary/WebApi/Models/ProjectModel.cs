using Domain;
using Domain.DomainUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class ProjectModel
    {
        public string Name { get; set; }

        public Project ToEntity() => new Project()
        {
            Name = this.Name,
        };
    }
}
