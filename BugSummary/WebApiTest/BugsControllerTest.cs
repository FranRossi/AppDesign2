using System.Collections;
using System.Collections.Generic;
using BusinessLogicInterface;
using DataAccess.Exceptions;
using Domain;
using Domain.DomainUtilities;
using KellermanSoftware.CompareNetObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TestUtilities;
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
        [TestMethod]
        public void AddValidBug(string token)
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
            Bug receivedBug = null;
            mock.Setup(m => m.Add(It.IsAny<string>(), It.IsAny<Bug>())).Callback((string token,Bug sentBug) =>
            {
                receivedBug = sentBug;
            });
            BugsController controller = new BugsController(mock.Object);

            IActionResult result = controller.Post(token,bug);

            mock.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(OkResult));
            CompareLogic compareLogic = new CompareLogic();
            ComparisonResult deepComparisonResult = compareLogic.Compare(bug.ToEntity(), receivedBug);
            Assert.IsTrue(deepComparisonResult.AreEqual);
        }
        
        [DataRow("1pojjYCG2Uj8WMXBteJYRqqcJZIS3dNL")]
        [DataTestMethod]
        [TestMethod]
        public void DeveloperAddsBug(string token)
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
            Bug receivedBug = null;
            mock.Setup(m => m.Add(It.IsAny<string>(), It.IsAny<Bug>())).Throws(new UserMustBeTesterException());
            BugsController controller = new BugsController(mock.Object);

            TestExceptionUtils.Throws<UserMustBeTesterException>(
                () => controller.Post(token,bug), "User's role must be tester for this action"
            );

        }
        
        
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
        public void UpdateValidBug(string token)
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
            Bug receivedBug = null;
            mock.Setup(m => m.Update(It.IsAny<string>(), It.IsAny<Bug>())).Callback((string token,Bug sentBug) =>
            {
                receivedBug = sentBug;
            });
            BugsController controller = new BugsController(mock.Object);

            IActionResult result = controller.Put(token,bug);

            mock.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(OkResult));
            CompareLogic compareLogic = new CompareLogic();
            ComparisonResult deepComparisonResult = compareLogic.Compare(bug.ToEntity(), receivedBug);
            Assert.IsTrue(deepComparisonResult.AreEqual);
        }
        
        [DataRow("1pojjYCG2Uj8WMXBteJYRqqcJZIS3dNL")]
        [DataTestMethod]
        [TestMethod]
        public void DeveloperUpdateBug(string token)
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
            Bug receivedBug = null;
            mock.Setup(m => m.Update(It.IsAny<string>(), It.IsAny<Bug>())).Throws(new UserMustBeTesterException());
            BugsController controller = new BugsController(mock.Object);

            TestExceptionUtils.Throws<UserMustBeTesterException>(
                () => controller.Put(token,bug), "User's role must be tester for this action"
            );

        }
        
        [DataRow("1pojjYCG2Uj8WMXBteJYRqqcJZIS3dNL")]
        [DataTestMethod]
        public void DeleteValidBug(string token)
        {
            int id = 1;
            int receivedId = -1;
            Mock<IBugLogic> mock = new Mock<IBugLogic>(MockBehavior.Strict);
            Bug receivedBug = null;
            mock.Setup(m => m.Delete(It.IsAny<string>(), It.IsAny<int>())).Callback((string token,int idSent) =>
            {
                receivedId = id;
            });
            BugsController controller = new BugsController(mock.Object);

            IActionResult result = controller.Delete(id, token);

            mock.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(OkResult));
            Assert.AreEqual(id, receivedId);
        }
        
        [DataRow("1pojjYCG2Uj8WMXBteJYRqqcJZIS3dNL")]
        [DataTestMethod]
        [TestMethod]
        public void DeveloperDeletesBug(string token)
        {
            int bugId = 1;
            Mock<IBugLogic> mock = new Mock<IBugLogic>(MockBehavior.Strict);
            mock.Setup(m => m.Delete(It.IsAny<string>(), It.IsAny<int>())).Throws(new UserMustBeTesterException());
            BugsController controller = new BugsController(mock.Object);

            TestExceptionUtils.Throws<UserMustBeTesterException>(
                () => controller.Delete(bugId, token), "User's role must be tester for this action"
            );

        }
        
    }
    
}
