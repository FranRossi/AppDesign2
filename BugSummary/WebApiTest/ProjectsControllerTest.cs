using BusinessLogicInterface;
using Domain;
using KellermanSoftware.CompareNetObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
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
            var projectModel = new ProjectModel
            {
                Name = "New Project 2022"
            };
            var ExpectedProject = new Project
            {
                Name = "New Project 2022"
            };

            var result = projectModel.ToEntity();
            var compareLogic = new CompareLogic();
            var deepComparisonResult = compareLogic.Compare(ExpectedProject, result);
            Assert.IsTrue(deepComparisonResult.AreEqual);
        }

        [TestMethod]
        public void AddValidProject()
        {
            var projectToAdd = new ProjectModel
            {
                Name = "New Project 2022"
            };
            var expectedProject = new Project
            {
                Name = "New Project 2022"
            };
            var mock = new Mock<IProjectLogic>(MockBehavior.Strict);
            Project receivedProject = null;
            mock.Setup(m => m.Add(It.IsAny<Project>())).Callback((Project project) => receivedProject = project);
            var controller = new ProjectsController(mock.Object);

            var result = controller.Post(projectToAdd);

            mock.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(OkResult));
            var compareLogic = new CompareLogic();
            var deepComparisonResult = compareLogic.Compare(expectedProject, receivedProject);
            Assert.IsTrue(deepComparisonResult.AreEqual);
        }

        [TestMethod]
        public void UpdateValidProject()
        {
            var projectToUpdate = new ProjectModel
            {
                Name = "New Project 2023"
            };
            var id = 1;
            var mock = new Mock<IProjectLogic>(MockBehavior.Strict);
            Project receivedProject = null;
            var receivedId = -1;
            mock.Setup(m => m.Update(It.IsAny<int>(), It.IsAny<Project>())).Callback((int id, Project sentProject) =>
            {
                receivedId = id;
                receivedProject = sentProject;
            });
            var controller = new ProjectsController(mock.Object);

            var result = controller.Post(id, projectToUpdate);

            mock.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(OkResult));
            var compareLogic = new CompareLogic();
            var deepComparisonResult = compareLogic.Compare(projectToUpdate.ToEntity(), receivedProject);
            Assert.IsTrue(deepComparisonResult.AreEqual);
            Assert.AreEqual(id, receivedId);
        }

        [TestMethod]
        public void DeleteValidProject()
        {
            var id = 1;
            var mock = new Mock<IProjectLogic>(MockBehavior.Strict);
            var receivedId = -1;
            mock.Setup(m => m.Delete(It.IsAny<int>())).Callback((int id) => { receivedId = id; });
            var controller = new ProjectsController(mock.Object);

            var result = controller.Delete(id);

            mock.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(OkResult));
            Assert.AreEqual(id, receivedId);
        }

        [TestMethod]
        public void AssignUserToProjectProject()
        {
            var projectId = 1;
            var userId = 1;
            var mock = new Mock<IProjectLogic>(MockBehavior.Strict);
            var receivedProjectId = -1;
            var receivedUserId = -1;
            mock.Setup(m => m.AssignUserToProject(It.IsAny<int>(), It.IsAny<int>())).Callback(
                (int sentUserId, int sentProjectId) =>
                {
                    receivedUserId = sentUserId;
                    receivedProjectId = sentProjectId;
                });
            var controller = new ProjectsController(mock.Object);

            var result = controller.Post(userId, projectId);

            mock.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(OkResult));
            Assert.AreEqual(projectId, receivedProjectId);
            Assert.AreEqual(userId, receivedUserId);
        }

        [TestMethod]
        public void DissociateUserFromProject()
        {
            var projectId = 1;
            var userId = 1;
            var mock = new Mock<IProjectLogic>(MockBehavior.Strict);
            var receivedProjectId = -1;
            var receivedUserId = -1;
            mock.Setup(m => m.DissociateUserFromProject(It.IsAny<int>(), It.IsAny<int>())).Callback(
                (int sentUserId, int sentProjectId) =>
                {
                    receivedUserId = sentUserId;
                    receivedProjectId = sentProjectId;
                });
            var controller = new ProjectsController(mock.Object);

            var result = controller.Delete(userId, projectId);

            mock.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(OkResult));
            Assert.AreEqual(projectId, receivedProjectId);
            Assert.AreEqual(userId, receivedUserId);
        }
    }
}