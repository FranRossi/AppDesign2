using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicInterface
{
    public interface IProjectLogic
    {
        public void Add(Project newProject);

        public void Update(int id, Project updatedProject);

        public void Delete(int projectId);

        public void AssignUserToProject(int userId, int projectId);

        public void DissociateUserFromProject(int userId, int projectId);
    }
}
