using System.Collections.Generic;
using Domain;

namespace DataAccessInterface
{
    public interface IProjectRepository
    {
        void Add(Project project);

        void Update(Project updatedProject);

        void Delete(int projectId);

        IEnumerable<Project> GetAll();
        Project Get(int projectId);

        void AssignUserToProject(int userId, int projectId);

        void DissociateUserFromProject(int userId, int projectId);

        void AddBugsFromFile(IEnumerable<Project> newProject);

        void Save();
    }
}