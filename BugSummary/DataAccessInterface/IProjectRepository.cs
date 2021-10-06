using Domain;

namespace DataAccessInterface
{
    public interface IProjectRepository : IRepository<Project>
    {
        public void Update(Project updatedProject);

        public void Delete(int projectId);

        public void AssignUserToProject(int projectId, int userId);

        public void DissociateUserFromProject(int userId, int projectId);

        public void AddBugsFromFile(Project newProject);
    }
}