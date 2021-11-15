using BusinessLogicInterface;
using Domain;
using KellermanSoftware.CompareNetObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using WebApi.Controllers;
using WebApi.Models;

namespace WebApiTest
{
    [TestClass]
    public class ProjectsControllerTest
    {

        [TestMethod]
        [ExcludeFromCodeCoverage]
        public void AddValidProject()
        {
            ProjectAddModel projectToAdd = new ProjectAddModel
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
            ProjectAddModel projectToUpdate = new ProjectAddModel
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

            IActionResult result = controller.Put(id, projectToUpdate);

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

        [TestMethod]
        public void AddBugsFromFile()
        {
            string path = "some path";
            string receivedPath = "";
            string companyName = "some company name";
            string receivedCompanyName = "";
            Mock<IProjectLogic> mock = new Mock<IProjectLogic>(MockBehavior.Strict);

            mock.Setup(m => m.AddBugsFromFile(It.IsAny<string>(), It.IsAny<string>())).Callback((string sentPath, string sentCompanyName) =>
            {
                receivedCompanyName = sentCompanyName;
                receivedPath = sentPath;
            });
            ProjectsController controller = new ProjectsController(mock.Object);

            IActionResult result = controller.Post(path, companyName);

            mock.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(OkResult));
            Assert.AreEqual(companyName, receivedCompanyName);
            Assert.AreEqual(path, receivedPath);
        }

        [TestMethod]
        public void GetBugsForUser()
        {
            IEnumerable<Project> projects = new List<Project>()
            {
                new Project
                    {
                        Name = "Project A",
                        Bugs = new List<Bug> { new Bug(), new Bug(), new Bug()},
                        Users = new List<User>{}
                    },
                new Project
                    {
                        Name = "Project B",
                        Bugs = new List<Bug> {  },
                        Users = new List<User>{}
                    },
                new Project
                    {
                        Name = "Project C",
                        Bugs = new List<Bug> { new Bug(), new Bug() },
                        Users = new List<User>{}
                    }
            };
            IEnumerable<ProjectModel> expectedModel = ProjectModel.ToModelList(projects);
            Mock<IProjectLogic> mock = new Mock<IProjectLogic>(MockBehavior.Strict);
            mock.Setup(r => r.GetAll()).Returns(projects);
            ProjectsController controller = new ProjectsController(mock.Object);

            IActionResult result = controller.Get();
            OkObjectResult okResult = result as OkObjectResult;
            IEnumerable<ProjectModel> projectResult = okResult.Value as IEnumerable<ProjectModel>;

            mock.VerifyAll();
            Assert.AreEqual(200, okResult.StatusCode);
            CompareLogic compareLogic = new CompareLogic();
            ComparisonResult deepComparisonResult = compareLogic.Compare(expectedModel, projectResult);
            Assert.IsTrue(deepComparisonResult.AreEqual);
        }

        [TestMethod]
        public void GetProjectById()
        {
            int projectId = 1;
            Project projectExpected = new Project
            {
                Id = projectId,
                Name = "Project A",
                Bugs = new List<Bug> { new Bug(), new Bug(), new Bug() },
                Users = new List<User> { new User(), new User(), },
                Assignments = new List<Assignment>{ new Assignment(), new Assignment()}
            };

            ProjectModel expectedModel = ProjectModel.ToModel(projectExpected);
            Mock<IProjectLogic> mock = new Mock<IProjectLogic>(MockBehavior.Strict);
            mock.Setup(r => r.Get(It.IsAny<int>())).Returns(projectExpected);
            ProjectsController controller = new ProjectsController(mock.Object);

            IActionResult result = controller.Get(projectId);
            OkObjectResult okResult = result as OkObjectResult;
            ProjectModel projectResult = okResult.Value as ProjectModel;

            mock.VerifyAll();
            Assert.AreEqual(200, okResult.StatusCode);
            CompareLogic compareLogic = new CompareLogic();
            ComparisonResult deepComparisonResult = compareLogic.Compare(expectedModel, projectResult);
            Assert.IsTrue(deepComparisonResult.AreEqual);
        }

    }
}