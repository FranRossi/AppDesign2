using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess
{
    public class ProjectRepository : BaseRepository<Project>
    {
        public ProjectRepository(BugSummaryContext bugSummaryContext)
        {
            Context = bugSummaryContext;
        }

        public override IEnumerable<Project> GetAll()
        {
            return Context.Projects.ToList();
        }
    }
}