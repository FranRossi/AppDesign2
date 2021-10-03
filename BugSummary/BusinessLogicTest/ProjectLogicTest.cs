﻿using BusinessLogic;
using DataAccessInterface;
using Domain;
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
    }
}
