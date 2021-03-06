using BusinessLogic;
using DataAccessInterface;
using Domain;
using Domain.DomainUtilities;
using KellermanSoftware.CompareNetObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using TestUtilities;

namespace BusinessLogicTest
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class UserLogicTest
    {
        [TestMethod]
        public void AddAdmin()
        {
            User newUser = new User
            {
                Id = 1,
                FirstName = "Pepe",
                LastName = "Perez",
                Password = "pepe1234",
                UserName = "pp",
                Email = "pepe@gmail.com",
                Role = RoleType.Admin
            };
            User receivedUser = null;
            Mock<IUserRepository> mockUserRepository = new Mock<IUserRepository>(MockBehavior.Strict);
            mockUserRepository.Setup(mr => mr.Add(It.IsAny<User>())).Callback((User newUser) =>
            {
                receivedUser = newUser;
            });
            mockUserRepository.Setup(mr => mr.Save());


            UserLogic userLogic = new UserLogic(mockUserRepository.Object);
            userLogic.Add(newUser);

            mockUserRepository.VerifyAll();
            Assert.AreEqual(newUser, receivedUser);
        }

        [TestMethod]
        public void AddDeveloper()
        {
            User newUser = new User
            {
                Id = 2,
                FirstName = "Juan",
                LastName = "Sanchez",
                Password = "contra 123",
                UserName = "juanito",
                Email = "juan@gmail.com",
                Role = RoleType.Developer
            };
            User receivedUser = null;
            Mock<IUserRepository> mockUserRepository = new Mock<IUserRepository>(MockBehavior.Strict);
            mockUserRepository.Setup(mr => mr.Add(It.IsAny<User>())).Callback((User newUser) =>
            {
                receivedUser = newUser;
            });
            mockUserRepository.Setup(mr => mr.Save());


            UserLogic userLogic = new UserLogic(mockUserRepository.Object);
            userLogic.Add(newUser);

            mockUserRepository.VerifyAll();
            Assert.AreEqual(newUser, receivedUser);
        }

        [TestMethod]
        public void AddTester()
        {
            User newUser = new User
            {
                Id = 24,
                FirstName = "Mario",
                LastName = "Fagundez",
                Password = "holasoyMario",
                UserName = "marito",
                Email = "mario@gmail.com",
                Role = RoleType.Tester
            };
            User receivedUser = null;
            Mock<IUserRepository> mockUserRepository = new Mock<IUserRepository>(MockBehavior.Strict);
            mockUserRepository.Setup(mr => mr.Add(It.IsAny<User>())).Callback((User newUser) =>
            {
                receivedUser = newUser;
            });
            mockUserRepository.Setup(mr => mr.Save());


            UserLogic userLogic = new UserLogic(mockUserRepository.Object);
            userLogic.Add(newUser);

            mockUserRepository.VerifyAll();
            Assert.AreEqual(newUser, receivedUser);
        }

        [DataRow("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpX")]
        [DataRow("ada2tsdarstda4545523apfd6Idtrsdd")]
        [DataTestMethod]
        public void GetUserByToken(string token)
        {
            User expectedUser = new User
            {
                Id = 0,
                FirstName = "Pepe",
                LastName = "Perez",
                Password = "pepe1234",
                UserName = "pp",
                Email = "pepe@gmail.com",
                Role = RoleType.Tester,
            };
            Mock<IUserRepository> mockUserRepository = new Mock<IUserRepository>(MockBehavior.Strict);
            mockUserRepository.Setup(m => m.Get(It.IsAny<string>())).Returns(expectedUser);


            UserLogic userLogic = new UserLogic(mockUserRepository.Object);
            User result = userLogic.Get(token);

            mockUserRepository.VerifyAll();
            Assert.AreEqual(expectedUser, result);
            CompareLogic compareLogic = new CompareLogic();
            ComparisonResult deepComparisonResult = compareLogic.Compare(expectedUser, result);
            Assert.IsTrue(deepComparisonResult.AreEqual);
        }

        [DataRow("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpX")]
        [DataRow("ada2tsdarstda4545523apfd6Idtrsdd")]
        [DataTestMethod]
        public void GetProjects(string token)
        {
            IEnumerable<Project> expectedProjects = new List<Project>
            {
                new Project{Id = 1, Name = "Project 1"},
                new Project{Id = 2, Name = "Project 2"},
                new Project{Id = 3, Name = "Project 3"},
            };
            Mock<IUserRepository> mockUserRepository = new Mock<IUserRepository>(MockBehavior.Strict);
            mockUserRepository.Setup(m => m.GetProjects(It.IsAny<string>())).Returns(expectedProjects);


            UserLogic userLogic = new UserLogic(mockUserRepository.Object);
            IEnumerable<Project> result = userLogic.GetProjects(token);

            mockUserRepository.VerifyAll();
            Assert.AreEqual(expectedProjects, result);
            CompareLogic compareLogic = new CompareLogic();
            ComparisonResult deepComparisonResult = compareLogic.Compare(expectedProjects, result);
            Assert.IsTrue(deepComparisonResult.AreEqual);
        }

        [TestMethod]
        public void GetAllUsers()
        {
            User newUser1 = new User
            {
                Id = 1,
                FirstName = "Pepe",
                LastName = "Perez",
                Password = "pepe1234",
                UserName = "pp",
                Email = "pepe@gmail.com",
                Role = RoleType.Tester,
                HourlyRate = 34,
            };
            User newUser2 = new User
            {
                Id = 2,
                FirstName = "Juan",
                LastName = "Gutierrez",
                Password = "juanoto",
                UserName = "llllllllllll",
                Email = "hola@gmail.com",
                Role = RoleType.Admin
            };
            User newUser3 = new User
            {
                Id = 3,
                FirstName = "Mario",
                LastName = "Kempes",
                Password = "marito24321",
                UserName = "pp",
                Email = "pepe@gmail.com",
                Role = RoleType.Developer,
                HourlyRate = 674,
            };
            IEnumerable<User> usersExpected = new List<User>()
            {
                newUser1, newUser2,newUser3
            };
            Mock<IUserRepository> mockBugRepository = new Mock<IUserRepository>(MockBehavior.Strict);
            mockBugRepository.Setup(mr => mr.GetAll()).Returns(usersExpected);

            UserLogic userLogic = new UserLogic(mockBugRepository.Object);
            IEnumerable<User> userResult = userLogic.GetAll();


            mockBugRepository.VerifyAll();
            CompareLogic compareLogic = new CompareLogic();
            ComparisonResult deepComparisonResult = compareLogic.Compare(usersExpected, userResult);
            Assert.IsTrue(deepComparisonResult.AreEqual);
        }
    }
}