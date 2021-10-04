using BusinessLogic;
using DataAccess;
using DataAccessInterface;
using Domain;
using Domain.DomainUtilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections;
using System.Collections.Generic;
using DataAccess.Exceptions;
using TestUtilities;
using Utilities.Comparers;

namespace BusinessLogicTest
{
    [TestClass]
    public class BugLogicTest
    {
        [DataRow("1pojjYCG2Uj8WMXBteJYRqqcJZIS3dNL")]
        [DataTestMethod]
        [TestMethod]
        public void AddBug(string token)
        {
            Mock<BugSummaryContext> mockContext = new Mock<BugSummaryContext>(MockBehavior.Strict);
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
            Mock<IBugRepository> mockBugRepository = new Mock<IBugRepository>(MockBehavior.Strict);
            mockBugRepository.Setup(mr => mr.Add(It.IsAny<User>(), newBug)).Callback((User user, Bug postedBug) =>
            {
                receivedBug = postedBug;
            });
            mockBugRepository.Setup(mr => mr.Save());
            Mock<IUserRepository> mockUserRepository = new Mock<IUserRepository>(MockBehavior.Strict);
            mockUserRepository.Setup(mr => mr.Get(It.IsAny<string>())).Returns(testerUser);
            mockUserRepository.Setup(mr => mr.Save());
            
            BugLogic bugLogic = new BugLogic(mockBugRepository.Object, mockUserRepository.Object);
            bugLogic.Add(token, newBug);

            mockBugRepository.VerifyAll();
            Assert.AreEqual(newBug, receivedBug);
        }

        [DataRow("1pojjYCG2Uj8WMXBteJYRqqcJZIS3dNL")]
        [DataTestMethod]
        [TestMethod]
        public void DeveloperAddBugInvalidRole(string token)
        {
            User tester = null;
            Mock<IUserRepository> mockUserRepository = new Mock<IUserRepository>(MockBehavior.Strict);
            mockUserRepository.Setup(mr => mr.Get(It.IsAny<string>())).Returns(tester);
            mockUserRepository.Setup(mr => mr.Save());
            Bug updatedBug = new Bug
            {
                Id = 1,
                Name = "BugNuevo",
                Description = "Bug en el cliente",
                Version = "1.5",
                State = BugState.Done,
                ProjectId = 1
            };
            Mock<IBugRepository> mockBugRepository = new Mock<IBugRepository>(MockBehavior.Strict);
            mockBugRepository.Setup(mr => mr.Update(It.IsAny<User>(), It.IsAny<Bug>()))
                .Throws(new InexistentBugException());
            mockBugRepository.Setup(mr => mr.Save());

            BugLogic bugLogic = new BugLogic(mockBugRepository.Object, mockUserRepository.Object);
            TestExceptionUtils.Throws<InexistentBugException>(
                () => bugLogic.Update(token, updatedBug),
                "The bug to update does not exist on database, please enter a different bug"
            );
        }

        [DataRow("1pojjYCG2Uj8WMXBteJYRqqcJZIS3dNL")]
        [DataTestMethod]
        public void GetBugsForUser(string token)
        {
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
            
            IEnumerable<Bug> bugsExpected = new List<Bug>()
            {
                new Bug()
                {
                    Id = 1,
                    Name = "Bug2021",
                    Description = "ImportanteBug",
                    Project = projectTester,
                    State = BugState.Active,
                    Version = "2",
                    ProjectId = 1,
                }
            };
            Mock<IUserRepository> mockUserRepository = new Mock<IUserRepository>(MockBehavior.Strict);
            mockUserRepository.Setup(mr => mr.Get(It.IsAny<string>())).Returns(testerUser);
            mockUserRepository.Setup(mr => mr.Save());
            Mock<IBugRepository> mockBugRepository = new Mock<IBugRepository>(MockBehavior.Strict);
            mockBugRepository.Setup(mr => mr.GetAllByTester(It.IsAny<User>())).Returns(bugsExpected);
            
            BugLogic bugLogic = new BugLogic(mockBugRepository.Object, mockUserRepository.Object);
            IEnumerable<Bug> bugsResult = bugLogic.GetAll(token);

            
            mockBugRepository.VerifyAll();
            CollectionAssert.AreEqual((ICollection) bugsExpected, (ICollection) bugsResult, new BugComparer());
        }
        
        [DataRow("1pojjYCG2Uj8WMXBteJYRqqcJZIS3dNL")]
        [DataTestMethod]
        public void UpdateValidBug(string token)
        {
            Bug updatedBug = new Bug
            {
                Id = 1,
                Name = "BugNuevo",
                Description = "Bug en el cliente",
                Version = "1.5",
                State = BugState.Done,
                ProjectId = 1
            };
            User tester = null;
            Bug sentBugToBeUpdated = null;
            Mock<IBugRepository> mockBugRepository = new Mock<IBugRepository>(MockBehavior.Strict);
            mockBugRepository.Setup(mr => mr.Update(It.IsAny<User>(),It.IsAny<Bug>()))
                .Callback((User user,Bug bug) =>
                {
                    sentBugToBeUpdated = bug;
                }); ;
            mockBugRepository.Setup(mr => mr.Save());
            Mock<IUserRepository> mockUserRepository = new Mock<IUserRepository>(MockBehavior.Strict);
            mockUserRepository.Setup(mr => mr.Get(It.IsAny<string>())).Returns(tester);
            mockUserRepository.Setup(mr => mr.Save());
            
            BugLogic bugLogic = new BugLogic(mockBugRepository.Object, mockUserRepository.Object);
            bugLogic.Update(token,updatedBug);

            mockBugRepository.VerifyAll();
            Assert.AreEqual(updatedBug, sentBugToBeUpdated);
        }

        [DataRow("1pojjYCG2Uj8WMXBteJYRqqcJZIS3dNL")]
        [DataTestMethod]
        [TestMethod]
        public void UpdateInvalidBug(string token)
        {
            User tester = null;
            Mock<IUserRepository> mockUserRepository = new Mock<IUserRepository>(MockBehavior.Strict);
            mockUserRepository.Setup(mr => mr.Get(It.IsAny<string>())).Returns(tester);
            mockUserRepository.Setup(mr => mr.Save());
            Bug updatedBug = new Bug
            {
                Id = 1,
                Name = "BugNuevo",
                Description = "Bug en el cliente",
                Version = "1.5",
                State = BugState.Done,
                ProjectId = 1
            };
            Mock<IBugRepository> mockBugRepository = new Mock<IBugRepository>(MockBehavior.Strict);
            mockBugRepository.Setup(mr => mr.Update(It.IsAny<User>(),It.IsAny<Bug>())).Throws(new InexistentBugException());
            mockBugRepository.Setup(mr => mr.Save());

            BugLogic bugLogic = new BugLogic(mockBugRepository.Object, mockUserRepository.Object);
            TestExceptionUtils.Throws<InexistentBugException>(
                () => bugLogic.Update(token,updatedBug), "The bug to update does not exist on database, please enter a different bug"
            );
        }
    }
}
