using BusinessLogicInterface;
using DataAccessInterface;
using Domain;
using System;
using System.Collections.Generic;

namespace BusinessLogic
{
    public class ProjectLogic : IProjectLogic
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectLogic(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public void Add(Project newProject)
        {
            _projectRepository.Add(newProject);
            _projectRepository.Save();
        }

        public void Update(int id, Project updatedProject)
        {
            updatedProject.Id = id;
            _projectRepository.Update(updatedProject);
            _projectRepository.Save();
        }

        public void Delete(int projectId)
        {
            _projectRepository.Delete(projectId);
            _projectRepository.Save();
        }

        public void AssignUserToProject(int userId, int projectId)
        {
            _projectRepository.AssignUserToProject(userId, projectId);
            _projectRepository.Save();
        }

        public void DissociateUserFromProject(int userId, int projectId)
        {
            _projectRepository.DissociateUserFromProject(userId, projectId);
            _projectRepository.Save();
        }

        public IEnumerable<Project> GetAll()
        {
            return _projectRepository.GetAll();
        }
    }
}