using Domain;
using Domain.DomainUtilities;
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
        public IEnumerable<Project> GetProjectFromFile(string path)
        {
            XDocument doc = XDocument.Load(path);

            Project project = new Project
            {
                Name = (string)doc.Descendants("Proyecto").First()
            };

            List<Bug> bugs = doc.Descendants("Bug").Select(x => new Bug
            {
                Name = (string)x.Element("Nombre"),
                Description = (string)x.Element("Descripcion"),
                Version = (string)x.Element("Version"),
                State = ((string)x.Element("Estado") == "Activo") ? BugState.Active : BugState.Fixed
            }).ToList();

            project.Bugs = bugs;
            IEnumerable<Project> result = new List<Project>() { project };
            return result;
        }
    }
}
