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
    public class ProjectControllerTest
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
            ProjectController controller = new ProjectController(mock.Object);

            IActionResult result = controller.Post(user);

            mock.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(OkResult));
            CompareLogic compareLogic = new CompareLogic();
            ComparisonResult deepComparisonResult = compareLogic.Compare(expectedProject, receivedProject);
            Assert.IsTrue(deepComparisonResult.AreEqual);
        }
    }
}
