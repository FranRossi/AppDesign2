using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace DataAccess
{
    public class BugRepository
    {
        private readonly DbContext _context;
        
        public BugRepository(DbContext bugSummaryContext)
        {
            this._context = bugSummaryContext;
        }

    }
}