using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Utilities;

namespace Testing
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
    }
}
