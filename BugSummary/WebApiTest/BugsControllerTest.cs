using System.Collections;
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
        
        [DataRow("1pojjYCG2Uj8WMXBteJYRqqcJZIS3dNL")]
        [DataTestMethod]
        public void GetBugsForTester(string token)
        {
            
            IEnumerable<Bug> bugsExpected = new List<Bug>()
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
            Mock<IBugLogic> mock = new Mock<IBugLogic>(MockBehavior.Strict);
            mock.Setup(r=>r.GetAll(It.IsAny<string>())).Returns(bugsExpected);
            BugsController controller = new BugsController(mock.Object);
            
            IActionResult result = controller.Get(token);
            OkObjectResult okResult = result as OkObjectResult;
            IEnumerable<Bug> bugsResult = okResult.Value as IEnumerable<Bug>;

            mock.VerifyAll();
            Assert.AreEqual(200,okResult.StatusCode);
            Assert.AreEqual(bugsExpected,bugsResult);
            CollectionAssert.AreEqual((ICollection) bugsExpected, (System.Collections.ICollection)bugsResult, new BugComparer());
        }
        
        [DataRow("1pojjYCG2Uj8WMXBteJYRqqcJZIS3dNL")]
        [DataTestMethod]
        [TestMethod]
        public void UpdateValiBug(string token)
        {
            BugModel bug = new BugModel
            {
                Name = "Bug2021",
                Description = "ImportanteBug",
                State = BugState.Active,
                Version = "2",
                ProjectId = 1,
            };
            Mock<IBugLogic> mock = new Mock<IBugLogic>(MockBehavior.Strict);
            User receivedBug = null;
            mock.Setup(m => m.Update(It.IsAny<string>(), It.IsAny<User>())).Callback((string token,User sentBug) =>
            {
                receivedBug = sentBug;
            });
            BugsController controller = new BugsController(mock.Object);

            IActionResult result = controller.Post(bug);

            mock.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(OkResult));
            CompareLogic compareLogic = new CompareLogic();
            ComparisonResult deepComparisonResult = compareLogic.Compare(bug.ToEntity(), receivedBug);
            Assert.IsTrue(deepComparisonResult.AreEqual);
        }
    }
}
