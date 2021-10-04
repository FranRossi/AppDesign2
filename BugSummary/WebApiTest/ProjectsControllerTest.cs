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

        [TestMethod]
        public void DeleteValidProject()
        {
            int id = 1;
            Mock<IProjectLogic> mock = new Mock<IProjectLogic>(MockBehavior.Strict);
            int receivedId = -1;
            mock.Setup(m => m.Delete(It.IsAny<int>())).Callback((int id) =>
            {
                receivedId = id;
            });
            ProjectsController controller = new ProjectsController(mock.Object);

            IActionResult result = controller.Delete(id);

            mock.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(OkResult));
            Assert.AreEqual(id, receivedId);
        }

        [TestMethod]
        public void AssignUserToProjectProject()
        {
            int projectId = 1;
            int userId = 1;
            Mock<IProjectLogic> mock = new Mock<IProjectLogic>(MockBehavior.Strict);
            int receivedProjectId = -1;
            int receivedUserId = -1;
            mock.Setup(m => m.AssignUserToProject(It.IsAny<int>(), It.IsAny<int>())).Callback((int sentUserId, int sentProjectId) =>
            {
                receivedUserId = sentUserId;
                receivedProjectId = sentProjectId;
            });
            ProjectsController controller = new ProjectsController(mock.Object);

            IActionResult result = controller.Post(userId, projectId);

            mock.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(OkResult));
            Assert.AreEqual(projectId, receivedProjectId);
            Assert.AreEqual(userId, receivedUserId);
        }

        [TestMethod]
        public void DissociateUserFromProject()
        {
            int projectId = 1;
            int userId = 1;
            Mock<IProjectLogic> mock = new Mock<IProjectLogic>(MockBehavior.Strict);
            int receivedProjectId = -1;
            int receivedUserId = -1;
            mock.Setup(m => m.DissociateUserFromProject(It.IsAny<int>(), It.IsAny<int>())).Callback((int sentUserId, int sentProjectId) =>
            {
                receivedUserId = sentUserId;
                receivedProjectId = sentProjectId;
            });
            ProjectsController controller = new ProjectsController(mock.Object);

            IActionResult result = controller.Delete(userId, projectId);

            mock.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(OkResult));
            Assert.AreEqual(projectId, receivedProjectId);
            Assert.AreEqual(userId, receivedUserId);
        }
    }
}
