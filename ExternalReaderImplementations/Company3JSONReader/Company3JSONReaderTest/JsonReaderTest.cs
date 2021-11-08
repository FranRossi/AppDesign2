using Company3JSONReader;
using ExternalReader;
using KellermanSoftware.CompareNetObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;

namespace FileHandlerTest
{
    [TestClass]
    public class JsonReaderTest
    {

        [TestMethod]
        public void ValidFileMultipleBugs()
        {
            string fileName = "empresa1_1.json";
            string debugPath = Path.Combine(Directory.GetParent(Environment.CurrentDirectory.ToString()).FullName);
            string path = debugPath.Substring(0, debugPath.IndexOf("Company3JSONReaderTest\\bin\\Debug"));
            string jsonPath = path + "TestUtilities\\BugFiles\\" + fileName;

            JSONReader companyReader = new JSONReader();
            IEnumerable<ProjectModel> result = companyReader.GetProjectsFromFile(new List<Parameter>{
                new Parameter
                {
                    Name = "Path",
                    Value = jsonPath
                }
            });

            IEnumerable<ProjectModel> expectedResult = new List<ProjectModel>() { GetFirstProyect() };
            CompareLogic compareLogic = new CompareLogic();
            ComparisonResult deepComparisonResult = compareLogic.Compare(expectedResult, result);
            Assert.IsTrue(deepComparisonResult.AreEqual);
        }

        [ExpectedException(typeof(UnableToReadException))]
        [TestMethod]
        public void WrongPath()
        {
            string jsonPath = "TestUtilities\\BugFile";

            JSONReader companyReader = new JSONReader();
            IEnumerable<ProjectModel> result = companyReader.GetProjectsFromFile(new List<Parameter>{
                new Parameter
                {
                    Name = "Path",
                    Value = jsonPath
                }
            });
        }

        [TestMethod]
        public void ValidFileOneBug()
        {
            string fileName = "empresa1_2.json";
            string debugPath = Path.Combine(Directory.GetParent(Environment.CurrentDirectory.ToString()).FullName);
            string path = debugPath.Substring(0, debugPath.IndexOf("Company3JSONReaderTest\\bin\\Debug"));
            string jsonPath = path + "TestUtilities\\BugFiles\\" + fileName;

            JSONReader companyReader = new JSONReader();
            IEnumerable<ProjectModel> result = companyReader.GetProjectsFromFile(new List<Parameter>{
                new Parameter
                {
                    Name = "Path",
                    Value = jsonPath
                }
            });

            IEnumerable<ProjectModel> expectedResult = new List<ProjectModel>() { GetSecondProyect() };
            CompareLogic compareLogic = new CompareLogic();
            ComparisonResult deepComparisonResult = compareLogic.Compare(expectedResult, result);
            Assert.IsTrue(deepComparisonResult.AreEqual);
        }

        private BugModel GetFirstBug()
        {
            return new BugModel
            {
                Name = "Error en el envío de correo",
                Description = "El error se produce cuando el usuario no tiene un correo asignado",
                Version = "1.0",
                State = BugState.Active
            };
        }

        private BugModel GetSecondBug()
        {
            return new BugModel
            {
                Name = "Error en el envío de correo 2",
                Description = "El error se produce cuando el usuario no tiene un correo asignado 2",
                Version = "1.0",
                State = BugState.Fixed
            };
        }

        private ProjectModel GetFirstProyect()
        {
            return new ProjectModel
            {
                Name = "Nombre del Proyecto",
                Bugs = new List<BugModel> { GetFirstBug(), GetSecondBug() }
            };
        }

        private ProjectModel GetSecondProyect()
        {
            return new ProjectModel
            {
                Name = "Nombre del Proyecto 2",
                Bugs = new List<BugModel> { GetSecondBug() }
            };
        }

    }
}

