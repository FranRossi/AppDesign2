using System.Collections.Generic;
using Domain;
using Domain.DomainUtilities;
using Domain.DomainUtilities.CustomExceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Testing
{
    [TestClass]
    public class UserTest
    {
        [TestMethod]
        public void CreateFirstName()
        {
            var newUser = new User
            {
                FirstName = "Juana"
            };
            Assert.AreEqual("Juana", newUser.FirstName);
        }

        [TestMethod]
        public void CreateLastName()
        {
            var newUser = new User
            {
                LastName = "DeArcos"
            };
            Assert.AreEqual("DeArcos", newUser.LastName);
        }

        [TestMethod]
        public void CreateUserName()
        {
            var newUser = new User
            {
                UserName = "jdArcos"
            };
            Assert.AreEqual("jdArcos", newUser.UserName);
        }

        [TestMethod]
        public void CreatePassword()
        {
            var newUser = new User
            {
                Password = "1234"
            };
            Assert.AreEqual("1234", newUser.Password);
        }

        [TestMethod]
        public void CreateEmail()
        {
            var newUser = new User
            {
                Email = "juana@gmail.com"
            };
            Assert.AreEqual("juana@gmail.com", newUser.Email);
        }

        [TestMethod]
        public void CreateRoleTester()
        {
            var newUser = new User
            {
                Role = RoleType.Tester
            };
            Assert.AreEqual(RoleType.Tester, newUser.Role);
        }

        [TestMethod]
        public void CreateRoleDeveloper()
        {
            var newUser = new User
            {
                Role = RoleType.Developer
            };
            Assert.AreEqual(RoleType.Developer, newUser.Role);
        }

        [TestMethod]
        public void CreateRoleAdmin()
        {
            var newUser = new User
            {
                Role = RoleType.Admin
            };
            Assert.AreEqual(RoleType.Admin, newUser.Role);
        }

        [TestMethod]
        public void CreateId()
        {
            var newUser = new User
            {
                Id = 1
            };
            Assert.AreEqual(1, newUser.Id);
        }

        [TestMethod]
        public void CreateToken()
        {
            var randomToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpX";
            var newUser = new User
            {
                Token = randomToken
            };
            Assert.AreEqual(randomToken, newUser.Token);
        }

        [TestMethod]
        public void CreateListProject()
        {
            var newUser = new User
            {
                Projects = new List<Project>()
            };
            Assert.IsNotNull(newUser.Projects);
        }

        [ExpectedException(typeof(UserPropertyIsNullException))]
        [TestMethod]
        public void VerifyFirstNameIsInCorrect()
        {
            var newUser = new User
            {
                FirstName = null
            };
        }

        [ExpectedException(typeof(UserPropertyIsNullException))]
        [TestMethod]
        public void VerifyLasttNameIsInCorrect()
        {
            var newUser = new User
            {
                LastName = null
            };
        }

        [ExpectedException(typeof(UserPropertyIsNullException))]
        [TestMethod]
        public void VerifyUserNameIsInCorrect()
        {
            var newUser = new User
            {
                UserName = null
            };
        }

        [ExpectedException(typeof(UserPropertyIsNullException))]
        [TestMethod]
        public void VerifyPasswordIsInCorrect()
        {
            var newUser = new User
            {
                Password = null
            };
        }

        [ExpectedException(typeof(EmailIsIncorrectException))]
        [TestMethod]
        public void VerifyEmailIsInCorrect()
        {
            var newUser = new User
            {
                Email = "estemail.estaMal@.ds@.com"
            };
        }


        [ExpectedException(typeof(UserRoleIncorrectException))]
        [TestMethod]
        public void VerifRoleIsInCorrect()
        {
            var newUser = new User
            {
                Role = (RoleType) (-1)
            };
        }
    }
}