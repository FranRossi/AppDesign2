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
            List<Bug> listBugForTester = new List<Bug>();
            foreach (var project in tester.Projects)
                listBugForTester.AddRange(Context.Bugs.ToList().FindAll(bug => bug.ProjectId == project.Id));
            return listBugForTester;
        }

        public void Add(User userToCreateBug, Bug newBug)
        {
            Project userProject = userToCreateBug.Projects.Find(p => p.Id == newBug.ProjectId);
            if (userProject == null)
                throw new ProjectDoesntBelongToUserException();
            Context.Bugs.Add(newBug);
        }

        public void Update(User testerUser, Bug updatedBug)
        {
            if (testerUser.Projects.Find(p => p.Id == updatedBug.ProjectId) == null)
                throw new ProjectDoesntBelongToUserException();
            Bug bugFromDb = Context.Bugs.Include("Project").FirstOrDefault(u => u.Id == updatedBug.Id);
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
            Bug bugFromDb = Context.Bugs.FirstOrDefault(b => b.Id == bugId);
            if (bugFromDb != null)
                Context.Bugs.Remove(bugFromDb);
            else
                throw new InexistentBugException();
        }

        public void FixBug(User developerUser, int bugId)
        {
            Bug bugFromDb = Context.Bugs.Include("Project").FirstOrDefault(u => u.Id == bugId);
            if (bugFromDb == null)
                throw new InexistentBugException();
            if (bugFromDb.State == BugState.Done)
                throw new BugAlreadyFixedException();
            if (developerUser.Projects.Find(p => p.Id == bugFromDb.ProjectId) == null)
                throw new ProjectDoesntBelongToUserException();
            bugFromDb.State = BugState.Done;
            bugFromDb.FixerId = developerUser.Id;
            Context.Bugs.Update(bugFromDb);

        }

        public Bug Get(int bugId)
        {
            Bug bugFromDb = Context.Bugs.FirstOrDefault(b => b.Id == bugId);
            if (bugFromDb != null)
                return bugFromDb;
            else
                throw new InexistentBugException();
        }
    }
}