using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace DataAccess
{
    public class BugRepository
    {
        private readonly DbContext _context;
        private readonly DbSet<Bug> _bugs;

        public BugRepository(BugSummaryContext bugSummaryContext)
        {
            this._context = bugSummaryContext;
            bugSummaryContext.Set<Bug>();
            this._bugs = bugSummaryContext.Bugs;
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