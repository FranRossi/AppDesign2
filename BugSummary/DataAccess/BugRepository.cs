using Domain;
using Domain.DomainUtilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess
{
    public class BugRepository : BaseRepository<Bug>
    {
        public BugRepository(BugSummaryContext bugSummaryContext)
        {
            Context = bugSummaryContext;
        }

        public override IEnumerable<Bug> GetAll()
        {
            return Context.Bugs.ToList();
        }

        public IEnumerable<Bug> GetAllByTesterID()
        {

            List<Bug> list = new List<Bug>
            {
            new Bug
            {
                Id = 2,
                Name = "Bug1",
                Description = "Bug en el servidor",
                Version = "1.4",
                State = BugState.Active,
                Project = new Project(),
                ProjectId = 1
            }

            };
            return list;
            //return Context.Bugs.ToList();
        }
    }
}