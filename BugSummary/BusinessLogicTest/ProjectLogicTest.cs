using BusinessLogic;
using DataAccessInterface;
using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

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
            Mock<IRepository<Project>> mockUserRepository = new Mock<IRepository<Project>>(MockBehavior.Strict);
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
    }
}
