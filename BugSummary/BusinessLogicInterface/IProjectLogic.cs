using Domain;
using ExternalReader;
using System;
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

        public void AddBugsFromFile(string path, string companyName);

        public IEnumerable<Project> GetAll();

        public IEnumerable<Tuple<string, IEnumerable<Parameter>>> GetExternalReadersInfo();

        public void AddBugsFromExternalReader(string externalReaderFileName, IEnumerable<Parameter> parameters);
      
        public Project Get(int projectId);
    }
}