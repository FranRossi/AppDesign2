using BusinessLogicInterface;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace WebApiTest
{
    [TestClass]
    public class UserControllerTest
    {
        [TestMethod]
        public void AddValidUser()
        {
            UserModel user = new UserModel
            {
                UserName = "Hola",
                Password = "Hola"
            };
            Mock<ILogic<User>> mock = new Mock<ILogic<User>>(MockBehavior.Strict);
            mock.Setup(m => m.Add(It.IsAny<User>()));
            UserController controller = new UserController(mock.Object);

            IActionResult result = controller.Post(user);

            mock.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }
    }
}
