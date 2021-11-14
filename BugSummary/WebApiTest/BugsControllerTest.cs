using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using BusinessLogicInterface;
using CustomExceptions;
using Domain;
using Domain.DomainUtilities;
using KellermanSoftware.CompareNetObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TestUtilities;
using Utilities.Comparers;
using Utilities.Criterias;
using WebApi.Controllers;
using WebApi.Models;

namespace WebApiTest
{

    [TestClass]
    [ExcludeFromCodeCoverage]
    public class BugsControllerTest
    {

        [TestMethod]
        public void AddValidBug()
        {
            string token = "1pojjYCG2Uj8WMXBteJYRqqcJZIS3dNL";
            BugModel bug = new BugModel
            {
                Id = 1,
                Name = "Bug2021",
                Description = "ImportanteBug",
                State = BugState.Active,
                Version = "2",
                FixingTime = 0,
                ProjectId = 1,
            };
            Mock<IBugLogic> mock = new Mock<IBugLogic>(MockBehavior.Strict);
            Bug receivedBug = null;
            mock.Setup(m => m.Add(It.IsAny<string>(), It.IsAny<Bug>())).Callback((string token, Bug sentBug) =>
            {
                receivedBug = sentBug;
            });
            BugsController controller = new BugsController(mock.Object);

            IActionResult result = controller.Post(token, bug);

            mock.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(OkResult));
            CompareLogic compareLogic = new CompareLogic();
            ComparisonResult deepComparisonResult = compareLogic.Compare(bug.ToEntity(), receivedBug);
            Assert.IsTrue(deepComparisonResult.AreEqual);
        }


        [TestMethod]
        public void GetValidBug()
        {
            string token = "1pojjYCG2Uj8WMXBteJYRqqcJZIS3dNL";
            int bugId = 1;
            Bug bugOnDataBase = new Bug
            {
                Id = bugId,
                Name = "Bug1",
                Description = "Bug en el servidor",
                Version = "1.4",
                State = BugState.Active,
                ProjectId = 1
            };
            Mock<IBugLogic> mock = new Mock<IBugLogic>(MockBehavior.Strict);
            Bug receivedBug = null;
            mock.Setup(m => m.Get(It.IsAny<string>(), It.IsAny<int>())).Returns(receivedBug = bugOnDataBase);
            BugsController controller = new BugsController(mock.Object);

            IActionResult result = controller.Get(token, bugId);
            OkObjectResult responseOk = result as OkObjectResult;
            BugModel bugResponse = responseOk.Value as BugModel;

            mock.VerifyAll();
            Assert.IsInstanceOfType(bugResponse, typeof(BugModel));
            Assert.AreEqual(0, new BugComparer().Compare(bugOnDataBase, receivedBug));
        }

        [TestMethod]
        public void GetBugsFilteredForUser()
        {
            string token = "1pojjYCG2Uj8WMXBteJYRqqcJZIS3dNL";
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
            IEnumerable<BugModel> expectedModels = BugModel.ToModelList(bugsExpected);
            BugSearchCriteria criteria = new BugSearchCriteria()
            {
                Name = "Bug1",
                State = BugState.Active,
                ProjectId = 1,
                Id = 1
            };
            Mock<IBugLogic> mock = new Mock<IBugLogic>(MockBehavior.Strict);
            mock.Setup(r => r.GetAllFiltered(It.IsAny<string>(), It.IsAny<BugSearchCriteria>())).Returns(bugsExpected);
            BugsController controller = new BugsController(mock.Object);

            IActionResult result = controller.GetAllFiltered(token, criteria);
            OkObjectResult okResult = result as OkObjectResult;
            IEnumerable<BugModel> bugsResult = okResult.Value as IEnumerable<BugModel>;

            mock.VerifyAll();
            Assert.AreEqual(200, okResult.StatusCode);
            CompareLogic compareLogic = new CompareLogic();
            ComparisonResult deepComparisonResult = compareLogic.Compare(expectedModels.First(), bugsResult.First());
            Assert.IsTrue(deepComparisonResult.AreEqual);
        }
        
        [TestMethod]
        public void GetBugsFilteredForUserOnlyBugState()
        {
            string token = "1pojjYCG2Uj8WMXBteJYRqqcJZIS3dNL";
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
            IEnumerable<BugModel> expectedModels = BugModel.ToModelList(bugsExpected);
            BugSearchCriteria criteria = new BugSearchCriteria()
            {
                State = BugState.Active,
            };
            Mock<IBugLogic> mock = new Mock<IBugLogic>(MockBehavior.Strict);
            mock.Setup(r => r.GetAllFiltered(It.IsAny<string>(), It.IsAny<BugSearchCriteria>())).Returns(bugsExpected);
            BugsController controller = new BugsController(mock.Object);

            IActionResult result = controller.GetAllFiltered(token, criteria);
            OkObjectResult okResult = result as OkObjectResult;
            IEnumerable<BugModel> bugsResult = okResult.Value as IEnumerable<BugModel>;

            mock.VerifyAll();
            Assert.AreEqual(200, okResult.StatusCode);
            CompareLogic compareLogic = new CompareLogic();
            ComparisonResult deepComparisonResult = compareLogic.Compare(expectedModels.First(), bugsResult.First());
            Assert.IsTrue(deepComparisonResult.AreEqual);
        }


        [TestMethod]
        public void UpdateValidBug()
        {
            string token = "1pojjYCG2Uj8WMXBteJYRqqcJZIS3dNL";
            int bugId = 1;
            BugModel bug = new BugModel
            {
                Id = 1,
                Name = "Bug2021",
                Description = "ImportanteBug",
                State = BugState.Active,
                Version = "2",
                ProjectId = 1,
            };
            Mock<IBugLogic> mock = new Mock<IBugLogic>(MockBehavior.Strict);
            Bug receivedBug = null;
            mock.Setup(m => m.Update(It.IsAny<string>(), It.IsAny<Bug>())).Callback((string token, Bug sentBug) =>
            {
                receivedBug = sentBug;
            });
            BugsController controller = new BugsController(mock.Object);

            IActionResult result = controller.Put(token, bug, bugId);

            mock.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(OkResult));
            CompareLogic compareLogic = new CompareLogic();
            ComparisonResult deepComparisonResult = compareLogic.Compare(bug.ToEntityWithID(bugId), receivedBug);
            Assert.IsTrue(deepComparisonResult.AreEqual);
        }

        [TestMethod]
        public void DeleteValidBug()
        {
            string token = "1pojjYCG2Uj8WMXBteJYRqqcJZIS3dNL";
            int id = 1;
            int receivedId = -1;
            Mock<IBugLogic> mock = new Mock<IBugLogic>(MockBehavior.Strict);
            mock.Setup(m => m.Delete(It.IsAny<string>(), It.IsAny<int>())).Callback((string token, int idSent) =>
            {
                receivedId = id;
            });
            BugsController controller = new BugsController(mock.Object);

            IActionResult result = controller.Delete(id, token);

            mock.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(OkResult));
            Assert.AreEqual(id, receivedId);
        }


        [TestMethod]
        public void FixBug()
        {
            int bug = 1;
            Mock<IBugLogic> mock = new Mock<IBugLogic>(MockBehavior.Strict);
            int receivedBug = -1;
            string token = "1pojjYCG2Uj8WMXBteJYRqqcJZIS3dNL";
            string receivedToken = "";
            int fixingTime = 2;
            int receivedFixingTime = -1;
            mock.Setup(m => m.Fix(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>())).Callback((string sentToken, int sentBug, int sentFixingTime) =>
            {
                receivedBug = sentBug;
                receivedToken = sentToken;
                receivedFixingTime = sentFixingTime;
            });
            BugsController controller = new BugsController(mock.Object);

            IActionResult result = controller.Patch(token, bug, fixingTime);

            mock.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(OkResult));
            Assert.AreEqual(bug, receivedBug);
            Assert.AreEqual(token, receivedToken);
        }
    }
}
