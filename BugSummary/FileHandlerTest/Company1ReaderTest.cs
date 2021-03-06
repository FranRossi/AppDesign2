using ExternalReader;
using FileHandler;
using FileHandlerInterface;
using KellermanSoftware.CompareNetObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using TestUtilities;

namespace FileHandlerTest
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class Company1ReaderTest
    {

        [TestMethod]
        public void ValidFile()
        {
            string fileName = "empresa1_1.XML";
            string debugPath = Path.Combine(Directory.GetParent(Environment.CurrentDirectory.ToString()).FullName);
            string path = debugPath.Substring(0, debugPath.IndexOf("FileHandlerTest\\bin\\Debug"));
            string xmlPath = path + "TestUtilities\\BugFiles\\" + fileName;

            IFileReaderStrategy companyReader = new Company1Reader();
            IEnumerable<ProjectModel> result = companyReader.GetProjectsFromFile(xmlPath);

            IEnumerable<ProjectModel> expectedResult = new List<ProjectModel>() { GetFirstProyect() };
            CompareLogic compareLogic = new CompareLogic();
            ComparisonResult deepComparisonResult = compareLogic.Compare(expectedResult, result);
            Assert.IsTrue(deepComparisonResult.AreEqual);
        }

        [TestMethod]
        public void OneBugValidFile()
        {
            string fileName = "empresa1_2.XML";
            string debugPath = Path.Combine(Directory.GetParent(Environment.CurrentDirectory.ToString()).FullName);
            string path = debugPath.Substring(0, debugPath.IndexOf("FileHandlerTest\\bin\\Debug"));
            string xmlPath = path + "TestUtilities\\BugFiles\\" + fileName;

            IFileReaderStrategy companyReader = new Company1Reader();
            IEnumerable<ProjectModel> result = companyReader.GetProjectsFromFile(xmlPath);

            IEnumerable<ProjectModel> expectedResult = new List<ProjectModel>() { GetSecondProyect() };
            CompareLogic compareLogic = new CompareLogic();
            ComparisonResult deepComparisonResult = compareLogic.Compare(expectedResult, result);
            Assert.IsTrue(deepComparisonResult.AreEqual);
        }


        [TestMethod]
        public void NoBugValidFile()
        {
            string fileName = "empresa1_3.XML";
            string debugPath = Path.Combine(Directory.GetParent(Environment.CurrentDirectory.ToString()).FullName);
            string path = debugPath.Substring(0, debugPath.IndexOf("FileHandlerTest\\bin\\Debug"));
            string xmlPath = path + "TestUtilities\\BugFiles\\" + fileName;

            IFileReaderStrategy companyReader = new Company1Reader();
            IEnumerable<ProjectModel> result = companyReader.GetProjectsFromFile(xmlPath);

            IEnumerable<ProjectModel> expectedResult = new List<ProjectModel>() { GetThirdProyect() };
            CompareLogic compareLogic = new CompareLogic();
            ComparisonResult deepComparisonResult = compareLogic.Compare(expectedResult, result);
            Assert.IsTrue(deepComparisonResult.AreEqual);
        }

        [TestMethod]
        public void InvalidFileStructure()
        {
            string fileName = "empresa1_4.XML";
            string debugPath = Path.Combine(Directory.GetParent(Environment.CurrentDirectory.ToString()).FullName);
            string path = debugPath.Substring(0, debugPath.IndexOf("FileHandlerTest\\bin\\Debug"));
            string xmlPath = path + "TestUtilities\\BugFiles\\" + fileName;

            IFileReaderStrategy companyReader = new Company1Reader();
            TestExceptionUtils.Throws<XmlException>(
                () => companyReader.GetProjectsFromFile(xmlPath),
                "Unexpected end of file has occurred. The following elements are not closed: Bug, Bugs, Empresa1. Line 16, position 32."
            );
        }

        private BugModel GetFirstBug()
        {
            return new BugModel
            {
                Name = "Error en el env?o de correo",
                Description = "El error se produce cuando el usuario no tiene un correo asignado",
                Version = "1.0",
                State = BugState.Active
            };
        }

        private BugModel GetSecondBug()
        {
            return new BugModel
            {
                Name = "Error en el env?o de correo 2",
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
                Name = "Nombre del Proyecto",
                Bugs = new List<BugModel> { GetFirstBug() }
            };
        }

        private ProjectModel GetThirdProyect()
        {
            return new ProjectModel
            {
                Name = "Nombre del Proyecto",
                Bugs = new List<BugModel> { }
            };
        }
    }
}

