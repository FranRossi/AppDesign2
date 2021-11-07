using BusinessLogicInterface;
using DataAccessInterface;
using Domain;
using ExternalReader;
using ExternalReaderImporterInterface;
using FileHandler;
using FileHandlerFactory;
using FileHandlerInterface;
using System;
using System.Collections.Generic;

namespace BusinessLogic
{
    public class ProjectLogic : IProjectLogic
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IExternalReaderImporter _externalReaderImporter;
        public ReaderFactory readerFactory { private get; set; }

        public ProjectLogic(IProjectRepository projectRepository, IExternalReaderImporter externalReaderImporter)
        {
            _projectRepository = projectRepository;
            _externalReaderImporter = externalReaderImporter;
            readerFactory = new ReaderFactory();
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
            IFileReaderStrategy readerStrategy = readerFactory.GetStrategy(companyName);
            IEnumerable<Project> parsedProject = readerStrategy.GetProjectsFromFile(path);
            _projectRepository.AddBugsFromFile(parsedProject);
            _projectRepository.Save();
        }

        public IEnumerable<Project> GetAll()
        {
            return _projectRepository.GetAll();
        }

        public IEnumerable<Tuple<string, IEnumerable<Parameter>>> GetExternalReadersInfo()
        {
            return _externalReaderImporter.GetExternalReadersInfo();
        }
    }
}