using FileHandler;
using ExternalReader;
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
using Utilities.CustomExceptions;

namespace FileHandlerTest
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class Company2ReaderTest
    {

        [TestMethod]
        public void ValidFile()
        {
            string fileName = "empresa2_1.txt";
            string debugPath = Path.Combine(Directory.GetParent(Environment.CurrentDirectory.ToString()).FullName);
            string path = debugPath.Substring(0, debugPath.IndexOf("FileHandlerTest\\bin\\Debug"));
            string txtPath = path + "TestUtilities\\BugFiles\\" + fileName;

            IFileReaderStrategy companyReader = new Company2Reader();
            IEnumerable<ProjectModel> result = companyReader.GetProjectsFromFile(txtPath);

            IEnumerable<ProjectModel> expectedResult = new List<ProjectModel> { GetFirstProyect() };
            CompareLogic compareLogic = new CompareLogic();
            ComparisonResult deepComparisonResult = compareLogic.Compare(expectedResult, result);
            Assert.IsTrue(deepComparisonResult.AreEqual);
        }

        [TestMethod]
        public void TwoProjectsValidFile()
        {
            string fileName = "empresa2_2.txt";
            string debugPath = Path.Combine(Directory.GetParent(Environment.CurrentDirectory.ToString()).FullName);
            string path = debugPath.Substring(0, debugPath.IndexOf("FileHandlerTest\\bin\\Debug"));
            string txtPath = path + "TestUtilities\\BugFiles\\" + fileName;

            IFileReaderStrategy companyReader = new Company2Reader();
            IEnumerable<ProjectModel> result = companyReader.GetProjectsFromFile(txtPath);

            IEnumerable<ProjectModel> expectedResult = new List<ProjectModel> { GetFirstProyect(), GetFourthProyect() };
            CompareLogic compareLogic = new CompareLogic();
            ComparisonResult deepComparisonResult = compareLogic.Compare(expectedResult, result);
            Assert.IsTrue(deepComparisonResult.AreEqual);
        }

        [TestMethod]
        public void InvalidFileStructure()
        {
            string fileName = "empresa2_3.txt";
            string debugPath = Path.Combine(Directory.GetParent(Environment.CurrentDirectory.ToString()).FullName);
            string path = debugPath.Substring(0, debugPath.IndexOf("FileHandlerTest\\bin\\Debug"));
            string txtPath = path + "TestUtilities\\BugFiles\\" + fileName;

            IFileReaderStrategy companyReader = new Company2Reader();
            TestExceptionUtils.Throws<InvalidCompany2BugFileException>(
                () => companyReader.GetProjectsFromFile(txtPath),
                "Unexpected end of file has occurred. Please check file structure and retry."
            );
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

        private ProjectModel GetFourthProyect()
        {
            return new ProjectModel
            {
                Name = "Nuevo1 del Proyecto",
                Bugs = new List<BugModel> { GetFirstBug(), GetSecondBug() }
            };
        }

    }
}

