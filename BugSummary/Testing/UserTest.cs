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
                firstName = "Juana"
            };
            Assert.AreEqual("Juana", newUser.firstName);
        }
        
        [TestMethod]
        public void CreateLastName()
        {
            User newUser = new User
            {
                lastName = "DeArcos"
            };
            Assert.AreEqual("DeArcos", newUser.lastName);
        }
        
        [TestMethod]
        public void CreateUserName()
        {
            User newUser = new User
            {
                userName = "jdArcos"
            };
            Assert.AreEqual("jdArcos", newUser.userName);
        }
        
        [TestMethod]
        public void CreatePassword()
        {
            User newUser = new User
            {
                password = "1234"
            };
            Assert.AreEqual("1234", newUser.password);
        }
        
        [TestMethod]
        public void CreateEmail()
        {
            User newUser = new User
            {
                email = "juana@gmail.com"
            };
            Assert.AreEqual("juana@gmail.com", newUser.email);
        }
        
        [TestMethod]
        public void CreateRoleTester()
        {
            User newUser = new User
            {
                role = RoleType.Tester
            };
            Assert.AreEqual(RoleType.Tester, newUser.role);
        }

        [TestMethod]
        public void CreateRoleDeveloper()
        {
            User newUser = new User
            {
                role = RoleType.Developer
            };
            Assert.AreEqual(RoleType.Developer, newUser.role);
        }

        [TestMethod]
        public void CreateRoleAdmin()
        {
            User newUser = new User
            {
                role = RoleType.Admin
            };
            Assert.AreEqual(RoleType.Admin, newUser.role);
        }
    }
}
