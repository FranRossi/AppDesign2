using ExternalReader;
using FileHandlerInterface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace FileHandler
{
    public class Company1Reader : IFileReaderStrategy
    {
        public IEnumerable<ProjectModel> GetProjectsFromFile(string path)
        {
            XDocument doc = XDocument.Load(path);

            ProjectModel ProjectModel = new ProjectModel
            {
                Name = (string)doc.Descendants("Proyecto").First()
            };

            List<BugModel> bugs = doc.Descendants("Bug").Select(x => new BugModel
            {
                Name = (string)x.Element("Nombre"),
                Description = (string)x.Element("Descripcion"),
                Version = (string)x.Element("Version"),
                State = ((string)x.Element("Estado") == "Activo") ? BugState.Active : BugState.Fixed
            }).ToList();

            ProjectModel.Bugs = bugs;
            IEnumerable<ProjectModel> result = new List<ProjectModel>() { ProjectModel };
            return result;
        }
    }
}
