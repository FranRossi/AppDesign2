﻿using DataAccess.Exceptions;
using DataAccessInterface;
using Domain;
using Domain.DomainUtilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Domain.DomainUtilities.CustomExceptions;
using Utilities.CustomExceptions;

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
                throw new UserMustBeTesterException();
        }

        public void Update(User testerUser, Bug updatedBug)
        {
            if (testerUser.Role != RoleType.Tester)
                throw new UserMustBeTesterException();
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
                throw new InexistentBugException();

        }

        public void FixBug(User developerUser, int bugId)
        {
            Bug bugFromDb = Context.Bugs.Include("Project").FirstOrDefault(u => u.Id == bugId);
            if (bugFromDb == null)
                throw new InexistentBugException();
            if (developerUser.Projects.Find(p => p.Id == bugFromDb.ProjectId) == null)
                throw new ProjectDoesntBelongToUserException();
            if (developerUser.Role != RoleType.Developer)
                throw new UserMustBeTesterException();
            bugFromDb.State = BugState.Done;
            bugFromDb.Fixer = developerUser;
            bugFromDb.Fixer.Projects = null;
            Context.Bugs.Update(bugFromDb);

        }
    }
}