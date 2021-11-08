using Domain;
using Domain.DomainUtilities;
using FileHandlerInterface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Utilities.CustomExceptions;

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

        public IEnumerable<Project> GetProjectsFromFile(string path)
        {
            string rawFile = File.ReadAllText(@path);
            string file = rawFile.Replace("\n", "").Replace("\r", "");
            List<Project> projects = new List<Project>();
            int chunkSize = GetTotalLength();
            if (file.Length % chunkSize != 0)
                throw new InvalidCompany2BugFileException();

            IEnumerable<string> splittedString = Split(file, chunkSize);
            foreach (string entry in splittedString)
            {
                int index = 0;
                string projectName = entry.Substring(index, _proyectNameLength).TrimEnd();
                index += _proyectNameLength;
                Project project = projects.Find(p => p.Name == projectName);
                if (project == null)
                {
                    project = new Project
                    {
                        Name = projectName,
                        Bugs = new List<Bug>()
                    };
                    projects.Add(project);
                }
                index += _idLength;
                Bug bug = new Bug();
                bug.Name = entry.Substring(index, _nameLength).TrimEnd();
                index += _nameLength;
                bug.Description = entry.Substring(index, _descriptionLength).TrimEnd();
                index += _descriptionLength;
                bug.Version = entry.Substring(index, _versionLength).TrimEnd();
                index += _versionLength;
                bug.State = (entry.Substring(index, _stateLength).TrimEnd() == "Activo") ? BugState.Active : BugState.Fixed;
                project.Bugs.Add(bug);
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
