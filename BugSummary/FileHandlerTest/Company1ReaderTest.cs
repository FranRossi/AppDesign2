using Domain;
using Domain.DomainUtilities;
using FileHandler;
using FileHandlerInterface;
using KellermanSoftware.CompareNetObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace FileHandlerTest
{
    [TestClass]
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
            IEnumerable<Project> result = companyReader.GetProjectFromFile(xmlPath);

            IEnumerable<Project> expectedResult = new List<Project>() { GetFirstProyect() };
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
            Project result = companyReader.GetProjectFromFile(xmlPath);

            IEnumerable<Project> expectedResult = new List<Project>() { GetSecondProyect() };
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
            Project result = companyReader.GetProjectFromFile(xmlPath);

            IEnumerable<Project> expectedResult = new List<Project>() { GetThirdProyect() };
            CompareLogic compareLogic = new CompareLogic();
            ComparisonResult deepComparisonResult = compareLogic.Compare(expectedResult, result);
            Assert.IsTrue(deepComparisonResult.AreEqual);
        }

        private Bug GetFirstBug()
        {
            return new Bug
            {
                Name = "Error en el envío de correo",
                Description = "El error se produce cuando el usuario no tiene un correo asignado",
                Version = "1.0",
                State = BugState.Active
            };
        }

        private Bug GetSecondBug()
        {
            return new Bug
            {
                Name = "Error en el envío de correo 2",
                Description = "El error se produce cuando el usuario no tiene un correo asignado 2",
                Version = "1.0",
                State = BugState.Active
            };
        }

        private Project GetFirstProyect()
        {
            return new Project
            {
                Name = "Nombre del Proyecto",
                Bugs = new List<Bug> { GetFirstBug(), GetSecondBug() }
            };
        }

        private Project GetSecondProyect()
        {
            return new Project
            {
                Name = "Nombre del Proyecto",
                Bugs = new List<Bug> { GetFirstBug() }
            };
        }

        private Project GetThirdProyect()
        {
            return new Project
            {
                Name = "Nombre del Proyecto",
                Bugs = new List<Bug> { }
            };
        }
    }
}

