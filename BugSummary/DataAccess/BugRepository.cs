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

        public IEnumerable<Bug> GetAllByTester(User tester)
        {
            List<Bug> listBugForTester = new List<Bug>();
            foreach (Project project in tester.Projects)
            {
                listBugForTester.AddRange(Context.Bugs.ToList().FindAll(bug => bug.ProjectId == project.Id));
            }
            return listBugForTester;
        }
    }
}