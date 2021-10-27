using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using BusinessLogic;
using CustomExceptions;
using DataAccess;
using DataAccessInterface;
using Domain;
using Domain.DomainUtilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TestUtilities;
using Utilities.Comparers;
using Utilities.Criterias;

namespace BusinessLogicTest
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class BugLogicTest
    {

        [TestMethod]
        public void GetBug()
        {
            string token = "1pojjYCG2Uj8WMXBteJYRqqcJZIS3dNL";
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
            mockBugRepository.Setup(mr => mr.Get(It.IsAny<User>(), It.IsAny<int>())).Returns(receivedBug = newBug);
            Mock<IUserRepository> mockUserRepository = new Mock<IUserRepository>(MockBehavior.Strict);
            mockUserRepository.Setup(mr => mr.Get(It.IsAny<string>())).Returns(testerUser);
            mockUserRepository.Setup(mr => mr.Save());

            BugLogic bugLogic = new BugLogic(mockBugRepository.Object, mockUserRepository.Object);
            bugLogic.Get(token, newBug.Id);

            mockBugRepository.VerifyAll();
            Assert.AreEqual(0, new BugComparer().Compare(newBug, receivedBug));
        }


        [TestMethod]
        public void AddBug()
        {
            string token = "1pojjYCG2Uj8WMXBteJYRqqcJZIS3dNL";
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
            Assert.AreEqual(0, new BugComparer().Compare(receivedBug, newBug));

        }


        [TestMethod]
        public void GetBugsFilteredForUser()
        {
            string token = "1pojjYCG2Uj8WMXBteJYRqqcJZIS3dNL";
            User testerUser = new User
            {
                Id = 1,
                FirstName = "Pepe",
                LastName = "Perez",
                Password = "pepe1234",
                UserName = "pp",
                Email = "pepe@gmail.com",
                Role = RoleType.Developer,
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
                    Name = "Bug1",
                    Description = "ImportanteBug",
                    Project = projectTester,
                    State = BugState.Active,
                    Version = "2",
                    ProjectId = 1,
                }
            };
            BugSearchCriteria criteria = new BugSearchCriteria()
            {
                Name = "Bug1",
                State = BugState.Active,
                ProjectId = 1,
                Id = 1
            };
            Mock<IUserRepository> mockUserRepository = new Mock<IUserRepository>(MockBehavior.Strict);
            mockUserRepository.Setup(mr => mr.Get(It.IsAny<string>())).Returns(testerUser);
            mockUserRepository.Setup(mr => mr.Save());
            Mock<IBugRepository> mockBugRepository = new Mock<IBugRepository>(MockBehavior.Strict);
            mockBugRepository.Setup(mr => mr.GetAllFiltered(It.IsAny<User>(), It.IsAny<Func<Bug, bool>>())).Returns(bugsExpected);

            BugLogic bugLogic = new BugLogic(mockBugRepository.Object, mockUserRepository.Object);
            IEnumerable<Bug> bugsResult = bugLogic.GetAllFiltered(token, criteria);


            mockBugRepository.VerifyAll();
            CollectionAssert.AreEqual((ICollection)bugsExpected, (ICollection)bugsResult, new BugComparer());
        }


        [TestMethod]
        public void UpdateValidBug()
        {
            string token = "1pojjYCG2Uj8WMXBteJYRqqcJZIS3dNL";
            Bug updatedBug = new Bug
            {
                Id = 1,
                Name = "BugNuevo",
                Description = "Bug en el cliente",
                Version = "1.5",
                State = BugState.Fixed,
                ProjectId = 1
            };
            User tester = null;
            Bug sentBugToBeUpdated = null;
            Mock<IBugRepository> mockBugRepository = new Mock<IBugRepository>(MockBehavior.Strict);
            mockBugRepository.Setup(mr => mr.Update(It.IsAny<User>(), It.IsAny<Bug>()))
                .Callback((User user, Bug bug) =>
                {
                    sentBugToBeUpdated = bug;
                }); ;
            mockBugRepository.Setup(mr => mr.Save());
            Mock<IUserRepository> mockUserRepository = new Mock<IUserRepository>(MockBehavior.Strict);
            mockUserRepository.Setup(mr => mr.Get(It.IsAny<string>())).Returns(tester);
            mockUserRepository.Setup(mr => mr.Save());

            BugLogic bugLogic = new BugLogic(mockBugRepository.Object, mockUserRepository.Object);
            bugLogic.Update(token, updatedBug);

            mockBugRepository.VerifyAll();
            Assert.AreEqual(updatedBug, sentBugToBeUpdated);
        }


        [TestMethod]
        public void UpdateInvalidBug()
        {
            string token = "1pojjYCG2Uj8WMXBteJYRqqcJZIS3dNL";
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
                State = BugState.Fixed,
                ProjectId = 1
            };
            Mock<IBugRepository> mockBugRepository = new Mock<IBugRepository>(MockBehavior.Strict);
            mockBugRepository.Setup(mr => mr.Update(It.IsAny<User>(), It.IsAny<Bug>())).Throws(new InexistentBugException());
            mockBugRepository.Setup(mr => mr.Save());

            BugLogic bugLogic = new BugLogic(mockBugRepository.Object, mockUserRepository.Object);
            TestExceptionUtils.Throws<InexistentBugException>(
                () => bugLogic.Update(token, updatedBug), "The entered bug does not exist."
            );

        }


        [TestMethod]
        public void DeleteValidBug()
        {
            string token = "1pojjYCG2Uj8WMXBteJYRqqcJZIS3dNL";
            User tester = null;
            Mock<IUserRepository> mockUserRepository = new Mock<IUserRepository>(MockBehavior.Strict);
            mockUserRepository.Setup(mr => mr.Get(It.IsAny<string>())).Returns(tester);
            mockUserRepository.Setup(mr => mr.Save());
            Mock<IBugRepository> mockBugRepository = new Mock<IBugRepository>(MockBehavior.Strict);
            int bugId = 1;
            int receivedBugId = -1;
            mockBugRepository.Setup(mr => mr.Delete(It.IsAny<User>(), It.IsAny<int>()))
                .Callback((User user, int sentId) =>
                {
                    receivedBugId = sentId;
                });
            mockBugRepository.Setup(mr => mr.Save());

            BugLogic bugLogic = new BugLogic(mockBugRepository.Object, mockUserRepository.Object);
            bugLogic.Delete(token, bugId);

            mockBugRepository.VerifyAll();
            Assert.AreEqual(bugId, receivedBugId);
        }


        [TestMethod]
        public void DeleteInvalidProject()
        {
            string token = "1pojjYCG2Uj8WMXBteJYRqqcJZIS3dNL";
            User tester = null;
            Mock<IUserRepository> mockUserRepository = new Mock<IUserRepository>(MockBehavior.Strict);
            mockUserRepository.Setup(mr => mr.Get(It.IsAny<string>())).Returns(tester);
            mockUserRepository.Setup(mr => mr.Save());
            Mock<IBugRepository> mockBugRepository = new Mock<IBugRepository>(MockBehavior.Strict);
            int bugId = 1; ;
            mockBugRepository.Setup(mr => mr.Delete(It.IsAny<User>(), It.IsAny<int>())).Throws(new InexistentBugException());
            mockBugRepository.Setup(mr => mr.Save());

            BugLogic bugLogic = new BugLogic(mockBugRepository.Object, mockUserRepository.Object);
            TestExceptionUtils.Throws<InexistentBugException>(
                () => bugLogic.Delete(token, bugId), "The entered bug does not exist."
            );
        }
        
        [TestMethod]
        public void FixValidBug()
        {
            string token = "1pojjYCG2Uj8WMXBteJYRqqcJZIS3dNL";
            int bugId = 1;
            User user = new User { UserName = "Pepe" };
            int receivedId = -1;
            User receivedUser = null;
            Mock<IBugRepository> mockBugRepository = new Mock<IBugRepository>(MockBehavior.Strict);
            mockBugRepository.Setup(mr => mr.Fix(It.IsAny<User>(), It.IsAny<int>()))
                .Callback((User user, int bug) =>
                {
                    receivedUser = user;
                    receivedId = bug;
                }); ;
            mockBugRepository.Setup(mr => mr.Save());
            Mock<IUserRepository> mockUserRepository = new Mock<IUserRepository>(MockBehavior.Strict);
            mockUserRepository.Setup(mr => mr.Get(It.IsAny<string>())).Returns(user);
            mockUserRepository.Setup(mr => mr.Save());

            BugLogic bugLogic = new BugLogic(mockBugRepository.Object, mockUserRepository.Object);
            bugLogic.Fix(token, bugId);

            mockBugRepository.VerifyAll();
            Assert.AreEqual(bugId, receivedId);
            Assert.AreEqual(user, receivedUser);
        }
    }
}