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

        public BugRepository(DbContext bugSummaryContext)
        {
            this._context = bugSummaryContext;
        }

        public void Create(Bug newBug)
        {
            return;
        }

        public IEnumerable<Bug> GetAll()
        {
            Bug newBug = new Bug
            {
                Id = 1,
                Name = "Bug1",
                Description = "Bug en el servidor",
                Version = "1.4",
                State = BugState.Active,
                ProjectId = new Project() { }
            };
            List<Bug> bugs = new List<Bug>();
            bugs.Add(newBug);
            return bugs;
        }


    }
}