using Domain;
using System.Collections.Generic;

namespace BusinessLogicInterface
{
    public interface IProjectLogic
    {
        public void Add(Project newProject);

        public void Update(int id, Project updatedProject);

        public void Delete(int projectId);

        public void AssignUserToProject(int userId, int projectId);

        public void DissociateUserFromProject(int userId, int projectId);
        public IEnumerable<Project> GetAll();
    }
}