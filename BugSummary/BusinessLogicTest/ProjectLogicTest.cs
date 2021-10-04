using BusinessLogic;
using DataAccessInterface;
using Domain;
using Domain.DomainUtilities.CustomExceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TestUtilities;
using Utilities.CustomExceptions;

namespace BusinessLogicTest
{
    [TestClass]
    public class ProjectLogicTest
    {
        [TestMethod]
        public void AddProject()
        {
            Project projectToAdd = new Project
            {
                Name = "New Project 2022"
            };
            Project receivedProject = null;
            Mock<IProjectRepository> mockUserRepository = new Mock<IProjectRepository>(MockBehavior.Strict);
            mockUserRepository.Setup(mr => mr.Add(It.IsAny<Project>())).Callback((Project newProject) =>
            {
                receivedProject = newProject;
            });
            mockUserRepository.Setup(mr => mr.Save());


            ProjectLogic projectLogic = new ProjectLogic(mockUserRepository.Object);
            projectLogic.Add(projectToAdd);

            mockUserRepository.VerifyAll();
            Assert.AreEqual(projectToAdd, receivedProject);
        }

        [TestMethod]
        public void AddAlreadyAddedProject()
        {
            Project projectToAdd = new Project
            {
                Name = "New Project 2022"
            };
            Mock<IProjectRepository> mockUserRepository = new Mock<IProjectRepository>(MockBehavior.Strict);
            mockUserRepository.Setup(mr => mr.Add(It.IsAny<Project>())).Throws(new ProjectNameIsNotUniqueException());

            ProjectLogic projectLogic = new ProjectLogic(mockUserRepository.Object);
            TestExceptionUtils.Throws<ProjectNameIsNotUniqueException>(
                () => projectLogic.Add(projectToAdd), "The project name chosen was already taken, please enter a different name"
            );
        }

        [TestMethod]
        public void UpdateValidProject()
        {
            Mock<IProjectRepository> mockUserRepository = new Mock<IProjectRepository>(MockBehavior.Strict);
            Project sentProject = null;
            int id = 1;
            Project updatedProject = new Project
            {
                Name = "Project 01"
            };
            mockUserRepository.Setup(mr => mr.Update(It.IsAny<Project>()))
                .Callback((Project project) =>
                {
                    sentProject = project;
                }); ;
            mockUserRepository.Setup(mr => mr.Save());

            ProjectLogic _projectLogic = new ProjectLogic(mockUserRepository.Object);
            _projectLogic.Update(id, updatedProject);

            mockUserRepository.Verify(mock => mock.Update(It.IsAny<Project>()), Times.Once());
            updatedProject.Id = id;
            Assert.AreEqual(updatedProject, sentProject);
        }

        [TestMethod]
        public void UpdateInvalidProject()
        {
            Mock<IProjectRepository> mockUserRepository = new Mock<IProjectRepository>(MockBehavior.Strict);
            int id = 1;
            Project updatedProject = new Project
            {
                Name = "Project 01"
            };
            mockUserRepository.Setup(mr => mr.Update(It.IsAny<Project>())).Throws(new InexistentProjectException());
            mockUserRepository.Setup(mr => mr.Save());

            ProjectLogic _sessionLogic = new ProjectLogic(mockUserRepository.Object);
            TestExceptionUtils.Throws<InexistentProjectException>(
               () => _sessionLogic.Update(id, updatedProject), "The entered project does not exist."
            );
        }

        [TestMethod]
        public void DeleteValidProject()
        {
            Mock<IProjectRepository> mockUserRepository = new Mock<IProjectRepository>(MockBehavior.Strict);
            int projectId = 1;
            int receivedId = -1;
            mockUserRepository.Setup(mr => mr.Delete(It.IsAny<int>()))
                .Callback((int sentId) =>
                {
                    receivedId = sentId;
                });
            mockUserRepository.Setup(mr => mr.Save());

            ProjectLogic _projectLogic = new ProjectLogic(mockUserRepository.Object);
            _projectLogic.Delete(projectId);

            mockUserRepository.Verify(mock => mock.Delete(It.IsAny<int>()), Times.Once());
            Assert.AreEqual(projectId, receivedId);
        }

        [TestMethod]
        public void DeleteInvalidProject()
        {
            Mock<IProjectRepository> mockUserRepository = new Mock<IProjectRepository>(MockBehavior.Strict);
            int id = 1;
            mockUserRepository.Setup(mr => mr.Delete(It.IsAny<int>())).Throws(new InexistentProjectException());

            ProjectLogic _sessionLogic = new ProjectLogic(mockUserRepository.Object);
            TestExceptionUtils.Throws<InexistentProjectException>(
               () => _sessionLogic.Delete(id), "The entered project does not exist."
            );
        }

        [TestMethod]
        public void AssignUserToProjectTest()
        {
            Mock<IProjectRepository> mockProject = new Mock<IProjectRepository>(MockBehavior.Strict);
            int projectId = 1;
            int userId = 1;
            int receivedProjectId = -1;
            int receivedUserId = -1;
            mockProject.Setup(mr => mr.AssignUserToProject(It.IsAny<int>(), It.IsAny<int>()))
                .Callback((int sentUserId, int sentProjectId) =>
                {
                    receivedProjectId = sentProjectId;
                    receivedUserId = sentUserId;
                });
            mockProject.Setup(mr => mr.Save());

            ProjectLogic _projectLogic = new ProjectLogic(mockProject.Object);
            _projectLogic.AssignUserToProject(userId, projectId);

            mockProject.Verify(mock => mock.AssignUserToProject(It.IsAny<int>(), It.IsAny<int>()), Times.Once());
            Assert.AreEqual(projectId, receivedProjectId);
            Assert.AreEqual(userId, receivedUserId);
        }

        [TestMethod]
        public void AssignInvalidUserToProjectTest()
        {
            Mock<IProjectRepository> mockProject = new Mock<IProjectRepository>(MockBehavior.Strict);
            int projectId = 1;
            int userId = -11;
            mockProject.Setup(mr => mr.AssignUserToProject(It.IsAny<int>(), It.IsAny<int>())).Throws(new InexistentUserException());

            ProjectLogic _projectLogic = new ProjectLogic(mockProject.Object);
            TestExceptionUtils.Throws<InexistentUserException>(
               () => _projectLogic.AssignUserToProject(userId, projectId), "The entered user does not exist."
            );
        }

        [TestMethod]
        public void AssignUserToInvalidProjectTest()
        {
            Mock<IProjectRepository> mockProject = new Mock<IProjectRepository>(MockBehavior.Strict);
            int projectId = 1;
            int userId = -11;
            mockProject.Setup(mr => mr.AssignUserToProject(It.IsAny<int>(), It.IsAny<int>())).Throws(new InexistentProjectException());

            ProjectLogic _projectLogic = new ProjectLogic(mockProject.Object);
            TestExceptionUtils.Throws<InexistentProjectException>(
               () => _projectLogic.AssignUserToProject(userId, projectId), "The entered project does not exist."
            );
        }

        [TestMethod]
        public void AssignInvalidRoleUserToProjectTest()
        {
            Mock<IProjectRepository> mockProject = new Mock<IProjectRepository>(MockBehavior.Strict);
            int projectId = 1;
            int userId = -11;
            mockProject.Setup(mr => mr.AssignUserToProject(It.IsAny<int>(), It.IsAny<int>())).Throws(new InvalidProjectAssigneeRoleException());

            ProjectLogic _projectLogic = new ProjectLogic(mockProject.Object);
            TestExceptionUtils.Throws<InvalidProjectAssigneeRoleException>(
               () => _projectLogic.AssignUserToProject(userId, projectId), "Project asingnees must either be Developers or Testers."
            );
        }

        [TestMethod]
        public void DissociateUserFromProject()
        {
            Mock<IProjectRepository> mockProject = new Mock<IProjectRepository>(MockBehavior.Strict);
            int projectId = 1;
            int userId = 1;
            int receivedProjectId = -1;
            int receivedUserId = -1;
            mockProject.Setup(mr => mr.DissociateUserFromProject(It.IsAny<int>(), It.IsAny<int>()))
                .Callback((int sentUserId, int sentProjectId) =>
                {
                    receivedProjectId = sentProjectId;
                    receivedUserId = sentUserId;
                });
            mockProject.Setup(mr => mr.Save());

            ProjectLogic _projectLogic = new ProjectLogic(mockProject.Object);
            _projectLogic.DissociateUserFromProject(userId, projectId);

            mockProject.Verify(mock => mock.DissociateUserFromProject(It.IsAny<int>(), It.IsAny<int>()), Times.Once());
            Assert.AreEqual(projectId, receivedProjectId);
            Assert.AreEqual(userId, receivedUserId);
        }
    }
}
