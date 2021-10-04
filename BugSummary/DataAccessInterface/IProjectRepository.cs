using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterface
{
    public interface IProjectRepository : IRepository<Project>
    {
        public void Update(Project updatedProject);

        public void Delete(int projectId);
    }
}
