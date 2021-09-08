using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Testing
{
    [TestClass]
    public class UserTest
    {
        [TestMethod]
        public void CreateUserName()
        {
            User newUser = new User
            {
                firstName = "Juana"
            };

            Assert.AreEqual("Juana", newUser.firstName);
        }
        
        [TestMethod]
        public void CreatelastName()
        {
            User newUser = new User
            {
                lastName = "DeArcos"
            };
        
            Assert.AreEqual("DeArcos", newUser.lastName);
        }
    }
}
