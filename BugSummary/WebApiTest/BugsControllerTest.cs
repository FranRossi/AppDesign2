using System.Collections;
using System.Collections.Generic;
using BusinessLogicInterface;
using Domain;
using Domain.DomainUtilities;
using KellermanSoftware.CompareNetObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Utilities.Comparers;
using Utilities.Criterias;
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

        [DataRow("1pojjYCG2Uj8WMXBteJYRqqcJZIS3dNL")]
        [DataTestMethod]
        [TestMethod]
        public void GetValidBug(string token)
        {
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
        public void BugToModelTest()
        {
            Bug expectedBug = new Bug
            {
                Id = 1,
                Name = "Bug1",
                Description = "Bug en el servidor",
                Version = "1.4",
                State = BugState.Active,
                ProjectId = 1,

            };
            BugModel bugToCompare = new BugModel
            {
                Id = 1,
                Name = "Bug1",
                Description = "Bug en el servidor",
                Version = "1.4",
                State = BugState.Active,
                ProjectId = 1,
            };
            BugModel model = BugModel.ToModel(expectedBug);
            CompareLogic compareLogic = new CompareLogic();
            ComparisonResult deepComparisonResult = compareLogic.Compare(model, bugToCompare);
            Assert.IsTrue(deepComparisonResult.AreEqual);
        }


        /*[DataRow("1pojjYCG2Uj8WMXBteJYRqqcJZIS3dNL")]
        [DataTestMethod]
        public void GetBugsForUser(string token)
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
            mock.Setup(r => r.GetAll(It.IsAny<string>())).Returns(bugsExpected);
            BugsController controller = new BugsController(mock.Object);

            IActionResult result = controller.GetAll(token);
            OkObjectResult okResult = result as OkObjectResult;
            IEnumerable<Bug> bugsResult = okResult.Value as IEnumerable<Bug>;

            mock.VerifyAll();
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(bugsExpected, bugsResult);
            CollectionAssert.AreEqual((ICollection)bugsExpected, (System.Collections.ICollection)bugsResult, new BugComparer());
        }*/

        [DataRow("1pojjYCG2Uj8WMXBteJYRqqcJZIS3dNL")]
        [DataTestMethod]
        public void GetBugsFilteredForUser(string token)
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
            IEnumerable<Bug> bugsResult = okResult.Value as IEnumerable<Bug>;

            mock.VerifyAll();
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(bugsExpected, bugsResult);
            CollectionAssert.AreEqual((ICollection)bugsExpected, (System.Collections.ICollection)bugsResult, new BugComparer());
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
            mock.Setup(m => m.Update(It.IsAny<string>(), It.IsAny<Bug>())).Callback((string token, Bug sentBug) =>
            {
                receivedBug = sentBug;
            });
            BugsController controller = new BugsController(mock.Object);

            IActionResult result = controller.Put(token, bug);

            mock.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(OkResult));
            CompareLogic compareLogic = new CompareLogic();
            ComparisonResult deepComparisonResult = compareLogic.Compare(bug.ToEntity(), receivedBug);
            Assert.IsTrue(deepComparisonResult.AreEqual);
        }

        [DataRow("1pojjYCG2Uj8WMXBteJYRqqcJZIS3dNL")]
        [DataTestMethod]
        public void DeleteValidBug(string token)
        {
            int id = 1;
            int receivedId = -1;
            Mock<IBugLogic> mock = new Mock<IBugLogic>(MockBehavior.Strict);
            Bug receivedBug = null;
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
            mock.Setup(m => m.FixBug(It.IsAny<string>(), It.IsAny<int>())).Callback((string sentToken, int sentBug) =>
            {
                receivedBug = sentBug;
                receivedToken = sentToken;
            });
            BugsController controller = new BugsController(mock.Object);

            IActionResult result = controller.Put(token, bug);

            mock.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(OkResult));
            Assert.AreEqual(bug, receivedBug);
            Assert.AreEqual(token, receivedToken);
        }

    }
}
