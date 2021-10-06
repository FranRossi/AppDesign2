using BusinessLogicInterface;
using DataAccessInterface;
using Domain;
using FileHandler;
using FileHandlerFactory;
using FileHandlerInterface;
using System;

namespace BusinessLogic
{
    public class ProjectLogic : IProjectLogic
    {
        private readonly IProjectRepository _projectRepository;
        private readonly ReaderFactory _readerFactory;

        public ProjectLogic(IProjectRepository projectRepository, ReaderFactory readerFactory)
        {
            _projectRepository = projectRepository;
            _readerFactory = readerFactory;
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

        public void AddBugsFromFile(string path, string companyName)
        {
            IFileReaderStrategy readerStrategy = _readerFactory.GetStrategy(companyName);
            Project parsedProject = readerStrategy.GetProjectFromFile(path);
            _projectRepository.AddBugsFromFile(parsedProject);
            _projectRepository.Save();
        }
    }
}