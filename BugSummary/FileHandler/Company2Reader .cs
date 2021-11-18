using ExternalReader;
using FileHandlerInterface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Utilities.CustomExceptions.FileHandler;

namespace FileHandler
{
    public class Company2Reader : IFileReaderStrategy
    {
        private const int _proyectNameLength = 30;
        private const int _idLength = 4;
        private const int _nameLength = 60;
        private const int _descriptionLength = 150;
        private const int _versionLength = 10;
        private const int _stateLength = 10;

        public IEnumerable<ProjectModel> GetProjectsFromFile(string path)
        {
            string rawFile = File.ReadAllText(@path);
            string file = rawFile.Replace("\n", "").Replace("\r", "");
            List<ProjectModel> projects = new List<ProjectModel>();
            int chunkSize = GetTotalLength();
            if (file.Length % chunkSize != 0)
                throw new InvalidCompany2BugFileException();

            IEnumerable<string> splittedString = Split(file, chunkSize);
            foreach (string entry in splittedString)
            {
                int index = 0;
                string projectName = entry.Substring(index, _proyectNameLength).TrimEnd();
                index += _proyectNameLength;
                ProjectModel ProjectModel = projects.Find(p => p.Name == projectName);
                if (ProjectModel == null)
                {
                    ProjectModel = new ProjectModel
                    {
                        Name = projectName,
                        Bugs = new List<BugModel>()
                    };
                    projects.Add(ProjectModel);
                }
                index += _idLength;
                BugModel BugModel = new BugModel();
                BugModel.Name = entry.Substring(index, _nameLength).TrimEnd();
                index += _nameLength;
                BugModel.Description = entry.Substring(index, _descriptionLength).TrimEnd();
                index += _descriptionLength;
                BugModel.Version = entry.Substring(index, _versionLength).TrimEnd();
                index += _versionLength;
                BugModel.State = (entry.Substring(index, _stateLength).TrimEnd() == "Activo") ? BugState.Active : BugState.Fixed;
                List<BugModel> bugList = (List<BugModel>)ProjectModel.Bugs;
                bugList.Add(BugModel);
                ProjectModel.Bugs = bugList;
            }
            return projects;
        }

        private int GetTotalLength()
        {
            return _proyectNameLength + _idLength + _nameLength + _descriptionLength + _versionLength + _stateLength;
        }

        private IEnumerable<string> Split(string str, int chunkSize)
        {
            return Enumerable.Range(0, str.Length / chunkSize)
                .Select(i => str.Substring(i * chunkSize, chunkSize));
        }
    }
}
