using DataAccess.Exceptions;
using DataAccessInterface;
using Domain;
using Domain.DomainUtilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess
{
    public class BugRepository : BaseRepository<Bug>, IBugRepository
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

        public void Add(User userToCreateBug, Bug newBug)
        {
            if (userToCreateBug.Role == RoleType.Tester)
            {
                foreach (Project project in userToCreateBug.Projects)
                {
                    if (project.Id == newBug.ProjectId)
                        Context.Bugs.Add(newBug);
                }
            }
            else
                throw new UserCannotCreateBugException();

        }
    }
}