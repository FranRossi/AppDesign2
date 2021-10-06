using System.Collections.Generic;
using System.Linq;
using DataAccessInterface;
using Domain;
using Microsoft.EntityFrameworkCore;
using Utilities.CustomExceptions;
namespace DataAccess
{
    public class ProjectRepository : BaseRepository<Project>, IProjectRepository
    {
        public ProjectRepository(BugSummaryContext bugSummaryContext)
        {
            Context = bugSummaryContext;
        }

        public override void Add(Project project)
        {
            if (!Context.Projects.Any(p => p.Name == project.Name))
                base.Add(project);
            else
                throw new ProjectNameIsNotUniqueException();
        }
        public override IEnumerable<Project> GetAllFiltered()
        {
            return Context.Projects.ToList();
        }

        public void Update(Project updatedProject)
        {
            Project projectFromDB = Context.Projects.FirstOrDefault(u => u.Id == updatedProject.Id);
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

        public void AssignUserToProject(int projectId, int userId)
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
    }
}
