using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace DataAccess
{
    public class ProjectRepository
    {
        private readonly DbContext _context;
        private readonly DbSet<Project> _projects;

        public ProjectRepository(BugSummaryContext bugSummaryContext)
        {
            this._context = bugSummaryContext;
            bugSummaryContext.Set<Project>();
            this._projects = bugSummaryContext.Projects;
        }

        public void Create(Project newProject)
        {
            this._projects.Add(newProject);
            this._context.SaveChanges();
        }

        public IEnumerable<Project> GetAll()
        {
            return this._projects;
        }
    }
}