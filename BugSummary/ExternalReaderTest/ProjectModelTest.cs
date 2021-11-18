using ExternalReader;
using KellermanSoftware.CompareNetObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace ExternalReaderTest
{
    [TestClass]
    public class ProjectModelTest
    {
        [TestMethod]
        public void CreateName()
        {
            ProjectModel newProject = new ProjectModel
            {
                Name = "Proyecto 01"
            };
            Assert.AreEqual("Proyecto 01", newProject.Name);
        }

        [TestMethod]
        public void CreateBugs()
        {
            BugModel newBug1 = new BugModel
            {
                Name = "Bug1"
            };
            BugModel newBug3 = new BugModel
            {
                Name = "Bug2"
            };
            BugModel newBug2 = new BugModel
            {
                Name = "Bug3"
            };
            IEnumerable<BugModel> expectedBugs = new List<BugModel> { newBug1, newBug2, newBug3 };
            ProjectModel newProject = new ProjectModel
            {
                Bugs = expectedBugs
            };

            CompareLogic compareLogic = new CompareLogic();
            ComparisonResult deepComparisonResult = compareLogic.Compare(expectedBugs, newProject.Bugs);
            Assert.IsTrue(deepComparisonResult.AreEqual);
        }
    }
}
