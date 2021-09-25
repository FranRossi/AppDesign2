using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Utilities;

namespace DataAccess
{
    public class BugRepository
    {
        private readonly DbContext _context;
        private readonly DbSet<Bug> _bugs;

        public BugRepository(DbContext bugSummaryContext)
        {
            this._context = bugSummaryContext;
            this._bugs = bugSummaryContext.Set<Bug>();
        }

        public void Create(Bug newBug)
        {
            this._bugs.Add(newBug);
            this._context.SaveChanges();
        }

        public IEnumerable<Bug> GetAll()
        {
            return this._bugs;
        }


    }
}