using BusinessLogicInterface;
using Domain;
using Domain.DomainUtilities;
using KellermanSoftware.CompareNetObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Utilities.Comparers;
using WebApi.Controllers;
using WebApi.Models;

namespace WebApiTest
{
    [TestClass]
    public class ProjectsControllerTest
    {
        [TestMethod]
        public void ProjectToEntityTest()
        {
            ProjectModel projectModel = new ProjectModel
            {
                Name = "New Project 2022"
            };
            Project ExpectedProject = new Project
            {
                Name = "New Project 2022"
            };

            Project result = projectModel.ToEntity();
            CompareLogic compareLogic = new CompareLogic();
            ComparisonResult deepComparisonResult = compareLogic.Compare(ExpectedProject, result);
            Assert.IsTrue(deepComparisonResult.AreEqual);
        }

        [TestMethod]
        public void AddValidProject()
        {
            ProjectModel projectToAdd = new ProjectModel
            {
                Name = "New Project 2022"
            };
            Project expectedProject = new Project
            {
                Name = "New Project 2022"
            };
            Mock<IProjectLogic> mock = new Mock<IProjectLogic>(MockBehavior.Strict);
            Project receivedProject = null;
            mock.Setup(m => m.Add(It.IsAny<Project>())).Callback((Project project) => receivedProject = project);
            ProjectsController controller = new ProjectsController(mock.Object);

            IActionResult result = controller.Post(projectToAdd);

            mock.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(OkResult));
            CompareLogic compareLogic = new CompareLogic();
            ComparisonResult deepComparisonResult = compareLogic.Compare(expectedProject, receivedProject);
            Assert.IsTrue(deepComparisonResult.AreEqual);
        }

        [TestMethod]
        public void UpdateValidProject()
        {
            ProjectModel projectToUpdate = new ProjectModel
            {
                Name = "New Project 2023"
            };
            int id = 1;
            Mock<IProjectLogic> mock = new Mock<IProjectLogic>(MockBehavior.Strict);
            Project receivedProject = null;
            int receivedId = -1;
            mock.Setup(m => m.Update(It.IsAny<int>(), It.IsAny<Project>())).Callback((int id, Project sentProject) =>
            {
                receivedId = id;
                receivedProject = sentProject;
            });
            ProjectsController controller = new ProjectsController(mock.Object);

            IActionResult result = controller.Post(id, projectToUpdate);

            mock.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(OkResult));
            CompareLogic compareLogic = new CompareLogic();
            ComparisonResult deepComparisonResult = compareLogic.Compare(projectToUpdate.ToEntity(), receivedProject);
            Assert.IsTrue(deepComparisonResult.AreEqual);
            Assert.AreEqual(id, receivedId);
        }
    }
}