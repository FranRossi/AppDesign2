﻿using BusinessLogic;
using DataAccessInterface;
using Domain;
using Domain.DomainUtilities;
using Domain.DomainUtilities.CustomExceptions;
using FileHandler;
using FileHandlerFactory;
using FileHandlerInterface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections;
using System.Collections.Generic;
using TestUtilities;
using Utilities.Comparers;
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


            ProjectLogic projectLogic = new ProjectLogic(mockUserRepository.Object, null);
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

            ProjectLogic projectLogic = new ProjectLogic(mockUserRepository.Object, null);
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

            ProjectLogic _projectLogic = new ProjectLogic(mockUserRepository.Object, null);
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

            ProjectLogic _sessionLogic = new ProjectLogic(mockUserRepository.Object, null);
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

            ProjectLogic _projectLogic = new ProjectLogic(mockUserRepository.Object, null);
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

            ProjectLogic _sessionLogic = new ProjectLogic(mockUserRepository.Object, null);
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

            ProjectLogic _projectLogic = new ProjectLogic(mockProject.Object, null);
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

            ProjectLogic _projectLogic = new ProjectLogic(mockProject.Object, null);
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

            ProjectLogic _projectLogic = new ProjectLogic(mockProject.Object, null);
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

            ProjectLogic _projectLogic = new ProjectLogic(mockProject.Object, null);
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

            ProjectLogic _projectLogic = new ProjectLogic(mockProject.Object, null);
            _projectLogic.DissociateUserFromProject(userId, projectId);

            mockProject.Verify(mock => mock.DissociateUserFromProject(It.IsAny<int>(), It.IsAny<int>()), Times.Once());
            Assert.AreEqual(projectId, receivedProjectId);
            Assert.AreEqual(userId, receivedUserId);
        }

        [TestMethod]
        public void DissociateInvalidUserFromProject()
        {
            Mock<IProjectRepository> mockProject = new Mock<IProjectRepository>(MockBehavior.Strict);
            int projectId = 1;
            int userId = -11;
            mockProject.Setup(mr => mr.DissociateUserFromProject(It.IsAny<int>(), It.IsAny<int>())).Throws(new InexistentUserException());

            ProjectLogic _projectLogic = new ProjectLogic(mockProject.Object, null);
            TestExceptionUtils.Throws<InexistentUserException>(
               () => _projectLogic.DissociateUserFromProject(userId, projectId), "The entered user does not exist."
            );
        }

        [TestMethod]
        public void DissociateUserFromInvalidProject()
        {
            Mock<IProjectRepository> mockProject = new Mock<IProjectRepository>(MockBehavior.Strict);
            int projectId = 1;
            int userId = -1;
            mockProject.Setup(mr => mr.DissociateUserFromProject(It.IsAny<int>(), It.IsAny<int>())).Throws(new InexistentProjectException());

            ProjectLogic _projectLogic = new ProjectLogic(mockProject.Object, null);
            TestExceptionUtils.Throws<InexistentProjectException>(
               () => _projectLogic.DissociateUserFromProject(userId, projectId), "The entered project does not exist."
            );
        }

        [TestMethod]
        public void AddBugsFromFile()
        {
            Project project = new Project
            {
                Name = "ProjectOne"
            };
            IEnumerable<Project> projects = new List<Project>() { project };
            IEnumerable<Project> receivedProjects = null;
            string path = "some path";
            string receivedPath = "";
            string companyName = "some company name";
            string receivedCompanyName = "";
            Mock<IProjectRepository> mockUserRepository = new Mock<IProjectRepository>(MockBehavior.Strict);
            Mock<ReaderFactory> mockReaderFactory = new Mock<ReaderFactory>(MockBehavior.Strict);
            Mock<IFileReaderStrategy> mockReader = new Mock<IFileReaderStrategy>(MockBehavior.Strict);
            mockReader.Setup(mf => mf.GetProjectFromFile(It.IsAny<string>())).Returns(projects)
                .Callback((string sentPath) => { receivedPath = sentPath; });
            mockReaderFactory.Setup(mf => mf.GetStrategy(It.IsAny<string>())).Returns(mockReader.Object)
                .Callback((string sentCompanyName) => { receivedCompanyName = sentCompanyName; });

            mockUserRepository.Setup(mr => mr.AddBugsFromFile(It.IsAny<IEnumerable<Project>>()))
                .Callback((IEnumerable<Project> sentProject) => { receivedProjects = sentProject; });
            mockUserRepository.Setup(mr => mr.Save());


            ProjectLogic projectLogic = new ProjectLogic(mockUserRepository.Object, mockReaderFactory.Object);
            projectLogic.AddBugsFromFile(path, companyName);

            mockUserRepository.VerifyAll();
            Assert.AreEqual(projects, receivedProjects);
            Assert.AreEqual(companyName, receivedCompanyName);
            Assert.AreEqual(path, receivedPath);
        }

        public void GetAllProjects()
        {
            Bug bug1 = new Bug
            {
                Name = "Bug1",
                Description = "Bug en el servidor",
                Version = "1.4",
                State = BugState.Active,
            };
            Bug bug2 = new Bug
            {
                Name = "Bug2",
                Description = "Bug en el servidor 22",
                Version = "1.5",
                State = BugState.Done,
            };
            Bug bug3 = new Bug
            {
                Name = "Bug3",
                Description = "Bug en el servidor 232",
                Version = "3.5",
                State = BugState.Done,
            };
            Project project1 = new Project
            {
                Name = "New Project 2022",
                Id = 1,
                Bugs = new List<Bug> { bug1, bug2 },
                Users = null
            };
            Project project2 = new Project
            {
                Name = "New Project 2022",
                Id = 1,
                Bugs = new List<Bug> { bug3 },
                Users = null
            };

            IEnumerable<Project> projectsExpected = new List<Project>()
            {
                project1, project2
            };
            Mock<IProjectRepository> mockBugRepository = new Mock<IProjectRepository>(MockBehavior.Strict);
            mockBugRepository.Setup(mr => mr.GetAll()).Returns(projectsExpected);

            ProjectLogic projectLogic = new ProjectLogic(mockBugRepository.Object, null);
            IEnumerable<Project> projectResult = projectLogic.GetAll();


            mockBugRepository.VerifyAll();
            CollectionAssert.AreEqual((ICollection)projectsExpected, (ICollection)projectResult, new ProjectComparer());
        }

        [TestMethod]
        public void GetAllProjectsOne()
        {
            Bug bug1 = new Bug
            {
                Name = "Bug1",
                Description = "Bug en el servidor",
                Version = "1.4",
                State = BugState.Active,
            };
            Bug bug2 = new Bug
            {
                Name = "Bug2",
                Description = "Bug en el servidor 22",
                Version = "1.5",
                State = BugState.Done,
            };
            Bug bug3 = new Bug
            {
                Name = "Bug3",
                Description = "Bug en el servidor 232",
                Version = "3.5",
                State = BugState.Done,
            };
            Project project1 = new Project
            {
                Name = "New Project 2022",
                Id = 1,
                Bugs = new List<Bug> { bug1, bug2, bug3 },
                Users = null
            };

            IEnumerable<Project> projectsExpected = new List<Project>()
            {
                project1
            };
            Mock<IProjectRepository> mockBugRepository = new Mock<IProjectRepository>(MockBehavior.Strict);
            mockBugRepository.Setup(mr => mr.GetAll()).Returns(projectsExpected);

            ProjectLogic projectLogic = new ProjectLogic(mockBugRepository.Object, null);
            IEnumerable<Project> projectResult = projectLogic.GetAll();


            mockBugRepository.VerifyAll();
            CollectionAssert.AreEqual((ICollection)projectsExpected, (ICollection)projectResult, new ProjectComparer());
        }
    }
}