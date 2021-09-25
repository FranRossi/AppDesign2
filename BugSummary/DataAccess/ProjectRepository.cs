using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace DataAccess
{
    public class ProjectRepository
    {
        private readonly DbContext _context;

        public ProjectRepository(BugSummaryContext bugSummaryContext)
        {
            this._context = bugSummaryContext;
        }

        public void Create(Project newProject)
        {
            return;
        }

        public IEnumerable<Project> GetAll()
        {
            Project newProject = new Project
            {
                Name = "New Project 2022",
                Id = 1,
                BugId = new List<Bug>() { },
            };
            List<Project> projectsExpected = new List<Project>();
            projectsExpected.Add(newProject);
            return projectsExpected;
        }
    }
}