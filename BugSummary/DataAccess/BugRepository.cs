using System;
using System.Collections.Generic;
using System.Linq;
using CustomExceptions;
using DataAccessInterface;
using Domain;
using Domain.DomainUtilities;
using Microsoft.EntityFrameworkCore;
using Utilities.CustomExceptions;

namespace DataAccess
{
    public class BugRepository : BaseRepository<Bug>, IBugRepository
    {
        public BugRepository(BugSummaryContext bugSummaryContext)
        {
            Context = bugSummaryContext;
        }

        public IEnumerable<Bug> GetAllFiltered(User user, Func<Bug, bool> criteria)
        {
            List<Bug> listBugForUser = new List<Bug>();
            foreach (var project in user.Projects)
                listBugForUser.AddRange(Context.Bugs.Where(criteria).ToList().FindAll(bug => bug.ProjectId == project.Id));
            return listBugForUser;
        }

        public void Add(User user, Bug newBug)
        {
            Project projectFromDb = Context.Projects.FirstOrDefault(u => u.Id == newBug.ProjectId);
            if (projectFromDb == null)
                throw new InexistentProjectException();
            if (!(user.Role == RoleType.Admin || user.Projects.Find(p => p.Id == newBug.ProjectId) != null))
                throw new UserIsNotAssignedToProjectException();
            Context.Bugs.Add(newBug);
        }

        public void Update(User user, Bug updatedBug)
        {
            Project projectFromDb = Context.Projects.FirstOrDefault(u => u.Id == updatedBug.ProjectId);
            if (projectFromDb == null)
                throw new InexistentProjectException();
            if (!(user.Role == RoleType.Admin || user.Projects.Find(p => p.Id == updatedBug.ProjectId) != null))
                throw new UserIsNotAssignedToProjectException();
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

        public void Delete(User user, int bugId)
        {
            Bug bugFromDb = Context.Bugs.Include("Project").FirstOrDefault(u => u.Id == bugId);
            if (bugFromDb == null)
                throw new InexistentBugException();
            if (!(user.Role == RoleType.Admin || user.Projects.Find(p => p.Id == bugFromDb.ProjectId) != null))
                throw new UserIsNotAssignedToProjectException();
            Context.Bugs.Remove(bugFromDb);
        }

        public void Fix(User user, int bugId, int fixingTime)
        {
            Bug bugFromDb = Context.Bugs.Include("Project").FirstOrDefault(u => u.Id == bugId);
            if (bugFromDb == null)
                throw new InexistentBugException();
            if (bugFromDb.State == BugState.Fixed)
                throw new BugAlreadyFixedException();
            if (!(user.Role == RoleType.Admin || user.Projects.Find(p => p.Id == bugFromDb.ProjectId) != null))
                throw new UserIsNotAssignedToProjectException();
            bugFromDb.State = BugState.Fixed;
            bugFromDb.FixerId = user.Id;
            Context.Bugs.Update(bugFromDb);

        }

        public Bug Get(User user, int bugId)
        {
            Bug bugFromDb = Context.Bugs.Include("Project").FirstOrDefault(u => u.Id == bugId);
            if (bugFromDb == null)
                throw new InexistentBugException();
            if (!(user.Role == RoleType.Admin || user.Projects.Find(p => p.Id == bugFromDb.ProjectId) != null))
                throw new UserIsNotAssignedToProjectException();
            return bugFromDb;

        }
    }
}