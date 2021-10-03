


using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Utilities.CustomExceptions;

namespace DataAccess
{
    public class ProjectRepository : BaseRepository<Project>
    {
        public ProjectRepository(BugSummaryContext bugSummaryContext)
        {
            Context = bugSummaryContext;
        }

        public override void Add(Project project)
        {
            if (!Context.Projects.Any(p => p.Name == project.Name))
                base.Add(project);
            else
                throw new ProjectNameIsNotUniqueException();
        }

        public override IEnumerable<Project> GetAll()
        {
            return Context.Projects.ToList();
        }
    }
}