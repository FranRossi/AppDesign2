using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
    }
}
