using System.Collections.Generic;
using Domain;

namespace DataAccessInterface
{
    public interface IProjectRepository : IRepository<Project>
    {
        public void Update(Project updatedProject);

        public void Delete(int projectId);

        public IEnumerable<Project> GetAll();

        public void AssignUserToProject(int userId, int projectId);

        public void DissociateUserFromProject(int userId, int projectId);

        public void AddBugsFromFile(IEnumerable<Project> newProject);
    }
}