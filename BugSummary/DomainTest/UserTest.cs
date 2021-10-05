using System.Collections.Generic;
using Domain;
using Domain.DomainUtilities;
using Domain.DomainUtilities.CustomExceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DomainTest
{
    [TestClass]
    public class UserTest
    {
        [TestMethod]
        public void CreateFirstName()
        {
            User newUser = new User
            {
                FirstName = "Juana"
            };
            Assert.AreEqual("Juana", newUser.FirstName);
        }

        [TestMethod]
        public void CreateLastName()
        {
            User newUser = new User
            {
                LastName = "DeArcos"
            };
            Assert.AreEqual("DeArcos", newUser.LastName);
        }

        [TestMethod]
        public void CreateUserName()
        {
            User newUser = new User
            {
                UserName = "jdArcos"
            };
            Assert.AreEqual("jdArcos", newUser.UserName);
        }

        [TestMethod]
        public void CreatePassword()
        {
            User newUser = new User
            {
                Password = "1234"
            };
            Assert.AreEqual("1234", newUser.Password);
        }

        [TestMethod]
        public void CreateEmail()
        {
            User newUser = new User
            {
                Email = "juana@gmail.com"
            };
            Assert.AreEqual("juana@gmail.com", newUser.Email);
        }

        [TestMethod]
        public void CreateRoleTester()
        {
            User newUser = new User
            {
                Role = RoleType.Tester
            };
            Assert.AreEqual(RoleType.Tester, newUser.Role);
        }

        [TestMethod]
        public void CreateRoleDeveloper()
        {
            User newUser = new User
            {
                Role = RoleType.Developer
            };
            Assert.AreEqual(RoleType.Developer, newUser.Role);
        }

        [TestMethod]
        public void CreateRoleAdmin()
        {
            User newUser = new User
            {
                Role = RoleType.Admin
            };
            Assert.AreEqual(RoleType.Admin, newUser.Role);
        }

        [TestMethod]
        public void CreateId()
        {
            User newUser = new User
            {
                Id = 1
            };
            Assert.AreEqual(1, newUser.Id);
        }

        [TestMethod]
        public void CreateToken()
        {
            string randomToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpX";
            User newUser = new User
            {
                Token = randomToken
            };
            Assert.AreEqual(randomToken, newUser.Token);
        }

        [TestMethod]
        public void CreateListProject()
        {
            User newUser = new User
            {
                Projects = new List<Project>()
            };
            Assert.IsNotNull(newUser.Projects);
        }

        [TestMethod]
        public void CreateListFixedBugs()
        {
            Bug bug = new Bug
            {
                Id = 1,
                Name = "Bug1",
                Description = "Bug en el servidor",
                Version = "1.4",
                State = BugState.Active,
                ProjectId = 1
            };
            User newUser = new User
            {
                FixedBugs = new List<Bug> { bug }
            };
            Assert.AreEqual(bug, newUser.FixedBugs[0]);
        }

        [ExpectedException(typeof(UserPropertyIsNullException))]
        [TestMethod]
        public void VerifyFirstNameIsInCorrect()
        {
            User newUser = new User
            {
                FirstName = null
            };
        }

        [ExpectedException(typeof(UserPropertyIsNullException))]
        [TestMethod]
        public void VerifyLasttNameIsInCorrect()
        {
            User newUser = new User
            {
                LastName = null
            };
        }

        [ExpectedException(typeof(UserPropertyIsNullException))]
        [TestMethod]
        public void VerifyUserNameIsInCorrect()
        {
            User newUser = new User
            {
                UserName = null
            };
        }

        [ExpectedException(typeof(UserPropertyIsNullException))]
        [TestMethod]
        public void VerifyPasswordIsInCorrect()
        {
            User newUser = new User
            {
                Password = null
            };
        }

        [ExpectedException(typeof(EmailIsIncorrectException))]
        [TestMethod]
        public void VerifyEmailIsInCorrect()
        {
            User newUser = new User
            {
                Email = "estemail.estaMal@.ds@.com"
            };
        }


        [ExpectedException(typeof(UserRoleIncorrectException))]
        [TestMethod]
        public void VerifRoleIsInCorrect()
        {
            User newUser = new User
            {
                Role = (RoleType)(-1)
            };
        }
    }
}