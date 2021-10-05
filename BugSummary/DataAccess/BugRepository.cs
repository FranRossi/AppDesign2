using System.Collections.Generic;
using System.Linq;
using DataAccess.Exceptions;
using DataAccessInterface;
using Domain;
using Domain.DomainUtilities;
using Microsoft.EntityFrameworkCore;

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
            var listBugForTester = new List<Bug>();
            foreach (var project in tester.Projects)
                listBugForTester.AddRange(Context.Bugs.ToList().FindAll(bug => bug.ProjectId == project.Id));
            return listBugForTester;
        }

        public void Add(User userToCreateBug, Bug newBug)
        {
            if (userToCreateBug.Role != RoleType.Tester)
                throw new UserMustBeTesterException();
            var userProject = userToCreateBug.Projects.Find(p => p.Id == newBug.ProjectId);
            if (userProject == null)
                throw new ProjectDontBelongToUser();
            Context.Bugs.Add(newBug);
        }

        public void Update(User testerUser, Bug updatedBug)
        {
            if (testerUser.Role != RoleType.Tester)
                throw new UserMustBeTesterException();
            if (testerUser.Projects.Find(p => p.Id == updatedBug.ProjectId) == null)
                throw new ProjectDontBelongToUser();
            var bugFromDb = Context.Bugs.Include("Project").FirstOrDefault(u => u.Id == updatedBug.Id);
            if (bugFromDb != null)
            {
                bugFromDb.Name = updatedBug.Name;
                bugFromDb.Description = updatedBug.Description;
                bugFromDb.ProjectId = updatedBug.ProjectId;
                bugFromDb.Version = updatedBug.Version;
                bugFromDb.State = updatedBug.State;
                Context.Bugs.Update(bugFromDb);
            }
            else
            {
                throw new InexistentBugException();
            }
        }

        public void Delete(User testerUser, int bugId)
        {
            if (testerUser.Role != RoleType.Tester)
                throw new UserMustBeTesterException();
            var bugFromDb = Context.Bugs.FirstOrDefault(b => b.Id == bugId);
            if (bugFromDb != null)
                Context.Bugs.Remove(bugFromDb);
            else
                throw new InexistentBugException();
        }
    }
}