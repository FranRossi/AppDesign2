using BusinessLogicInterface;
using DataAccessInterface;
using Domain;
using ExternalReader;
using BugReaderImporterInterface;
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
        private readonly IBugReaderImporter _externalReaderImporter;

        public ProjectLogic(IProjectRepository projectRepository, IBugReaderImporter externalReaderImporter)
        {
            _projectRepository = projectRepository;
            _externalReaderImporter = externalReaderImporter;
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

        public IEnumerable<Tuple<string, IEnumerable<Parameter>>> GetExternalReadersInfo()
        {
            return _externalReaderImporter.GetExternalReadersInfo();
        }

        public void AddBugsFromExternalReader(string externalReaderName, IEnumerable<Parameter> parameters)
        {
            IExternalReader externalReader = _externalReaderImporter.GetExternalReader(externalReaderName);
            IEnumerable<ProjectModel> projects = externalReader.GetProjectsFromFile(parameters);
            IEnumerable<Project> parsedProjects = ParseProjectModels(projects);
            _projectRepository.AddBugsFromFile(parsedProjects);
            _projectRepository.Save();
        }

        private IEnumerable<Project> ParseProjectModels(IEnumerable<ProjectModel> projects)
        {
            List<Project> parsedProjects = new List<Project>();
            foreach (ProjectModel project in projects)
            {
                List<Bug> parsedBugs = new List<Bug>();
                foreach (BugModel bug in project.Bugs)
                {
                    Bug newBug = new Bug
                    {
                        Name = bug.Name,
                        State = (Domain.DomainUtilities.BugState)(int)bug.State,
                        Description = bug.Description,
                        Version = bug.Version
                    };
                    parsedBugs.Add(newBug);
                }
                Project newProject = new Project
                {
                    Name = project.Name,
                    Bugs = parsedBugs
                };
                parsedProjects.Add(newProject);
            }
            return parsedProjects;
        }

        public Project Get(int projectId, string token)
        {
            return _projectRepository.Get(projectId, token);
        }
    }
}