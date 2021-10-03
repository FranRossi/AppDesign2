using System.Collections.Generic;
using BusinessLogicInterface;
using DataAccessInterface;
using Domain;
using Domain.DomainUtilities;
using KellermanSoftware.CompareNetObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Utilities.Comparers;
using WebApi.Controllers;
using WebApi.Models;

namespace WebApiTest
{
    [TestClass]
    public class BugsControllerTest
    {
        [TestMethod]
        public void GetBugsForTester(int id)
        {
            List<Bug> bugsExpected = new List<Bug>()
            {
                new Bug()
                {
                    Id = 1,
                    Name = "Bug2021",
                    Description = "ImportanteBug",
                    Project = new Project(),
                    State = BugState.Active,
                    Version = "2",
                    ProjectId = 1,
                }
            };
            Mock<IUserRepository> mock = new Mock<IUserRepository>(MockBehavior.Strict);
            mock.Setup(r=>r.GetAll()).Returns(bugsExpected);
            BugsController controller = new BugsController(mock.Object);
            
            mock.VerifyAll();
            IActionResult result = controller.Get();
            OkObjectResult okResult = result as OkObjectResult;
            IEnumerable<Bug> bugsResult = okResult.Value as IEnumerable<Bug>;

            Assert.AreEqual(200,okResult.StatusCode);
            Assert.AreEqual(bugsExpected,bugsResult);
            CollectionAssert.AreEqual(bugsExpected, (System.Collections.ICollection)bugsResult, new BugComparer());
        }
    }
}
