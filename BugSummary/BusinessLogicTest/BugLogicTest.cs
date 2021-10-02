using BusinessLogic;
using DataAccess;
using DataAccessInterface;
using Domain;
using Domain.DomainUtilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicTest
{
    [TestClass]
    public class BugLogicTest
    {

        [TestMethod]
        public void AddBug()
        {
            Mock<BugSummaryContext> _mockContext = new Mock<BugSummaryContext>(MockBehavior.Strict);
            User testerUser = new User
            {
                Id = 1,
                FirstName = "Pepe",
                LastName = "Perez",
                Password = "pepe1234",
                UserName = "pp",
                Email = "pepe@gmail.com",
                Role = RoleType.Tester,
                Projects = new List<Project>()
            };
            Project projectTester = new Project()
            {
                Id = 1,
                Name = "Semester 2021",
                Users = new List<User>
                {
                 testerUser
                }
            };
            testerUser.Projects.Add(projectTester);
            Bug newBug = new Bug
            {
                Id = 1,
                Name = "Bug1",
                Description = "Bug en el servidor",
                Version = "1.4",
                State = BugState.Active,
                Project = projectTester,
                ProjectId = 1
            };
            Bug receivedBug = null;
            Mock<IBugRepository> _mockUserRepository = new Mock<IBugRepository>(MockBehavior.Strict);
            _mockUserRepository.Setup(mr => mr.Add(It.IsAny<User>(), newBug)).Callback((User user, Bug postedBug) =>
            {
                receivedBug = postedBug;
            });
            _mockUserRepository.Setup(mr => mr.Save());


            BugLogic bugLogic = new BugLogic(_mockUserRepository.Object);
            bugLogic.Add(testerUser, newBug);

            _mockUserRepository.VerifyAll();
            Assert.AreEqual(newBug, receivedBug);
        }

    }
}
