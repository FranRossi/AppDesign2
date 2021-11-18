using System;
using System.Collections.Generic;
using System.Linq;
using DataAccessInterface;
using Domain;
using Domain.DomainUtilities;
using Microsoft.EntityFrameworkCore;
using Utilities.CustomExceptions.DataAccess;

namespace DataAccess
{
    public class ProjectRepository : BaseRepository<Project>, IProjectRepository
    {
        public ProjectRepository(BugSummaryContext bugSummaryContext)
        {
            Context = bugSummaryContext;
        }

        public void Add(Project project)
        {
            if (!Context.Projects.Any(p => p.Name == project.Name))
                Context.Projects.Add(project);
            else
                throw new ProjectNameIsNotUniqueException();
        }

        public IEnumerable<Project> GetAll()
        {
            return Context.Projects.Include("Bugs.Fixer").ToList();
        }

        public void Update(Project updatedProject)
        {
            if (Context.Projects.Any(p => p.Name == updatedProject.Name))
                throw new ProjectNameIsNotUniqueException();
            Project projectFromDB = Context.Projects.FirstOrDefault(p => p.Id == updatedProject.Id);
            if (projectFromDB != null)
            {
                projectFromDB.Name = updatedProject.Name;
                Context.Projects.Update(projectFromDB);
            }
            else
                throw new InexistentProjectException();
        }

        public void Delete(int projectId)
        {
            Project projectFromDB = Context.Projects.FirstOrDefault(u => u.Id == projectId);
            if (projectFromDB != null)
            {
                Context.Projects.Remove(projectFromDB);
            }
            else
                throw new InexistentProjectException();
        }

        public void AssignUserToProject(int userId, int projectId)
        {
            Project projectFromDB = Context.Projects.Include("Users").FirstOrDefault(u => u.Id == projectId);
            if (projectFromDB == null)
                throw new InexistentProjectException();
            User userFromDB = Context.Users.FirstOrDefault(u => u.Id == userId);
            if (userFromDB == null)
                throw new InexistentUserException();
            projectFromDB.AddUser(userFromDB);
            Context.Projects.Update(projectFromDB);
        }

        public void DissociateUserFromProject(int userId, int projectId)
        {
            Project projectFromDB = Context.Projects.Include("Users").FirstOrDefault(u => u.Id == projectId);
            if (projectFromDB == null)
                throw new InexistentProjectException();
            User userFromDB = Context.Users.FirstOrDefault(u => u.Id == userId);
            if (userFromDB == null)
                throw new InexistentUserException();
            projectFromDB.Users.Remove(userFromDB);
            Context.Projects.Update(projectFromDB);
        }

        public void AddBugsFromFile(IEnumerable<Project> projects)
        {
            foreach (Project newProject in projects)
            {
                Project projectFromDB = Context.Projects.FirstOrDefault(p => p.Name == newProject.Name);
                if (projectFromDB == null)
                    throw new InexistentProjectException();
                foreach (Bug newBug in newProject.Bugs)
                {
                    newBug.ProjectId = projectFromDB.Id;
                    Context.Bugs.Add(newBug);
                }
            }
        }

        public Project Get(int projectId, string token)
        {
            User user = Context.Users.FirstOrDefault(u => u.Token == token);
            Project project = Context.Projects.Include("Bugs.Fixer").Include("Assignments").Include("Users").FirstOrDefault(p => p.Id == projectId);
            if (user.Role == RoleType.Admin)
                return project;
            User UserInProject = project.Users.FirstOrDefault(u => u.Id == user.Id);
            if (UserInProject != null)
                return project;
            else
                throw new InexistentProjectException();

        }
    }
}
