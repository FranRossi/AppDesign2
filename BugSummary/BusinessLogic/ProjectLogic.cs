using DataAccessInterface;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class ProjectLogic
    {
        private IRepository<Project> _projectRepository;
        public ProjectLogic(IRepository<Project> projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public void Add(Project newProject)
        {
            _projectRepository.Add(newProject);
            _projectRepository.Save();
        }
    }
}
