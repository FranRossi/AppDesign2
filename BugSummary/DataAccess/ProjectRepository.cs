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
    }
}