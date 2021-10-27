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
    public class AssignmentControllerTest
    {

        [TestMethod]
        public void AddValidAssignment()
        {
            Project projectTest = new Project()
            {
                Id = 1,
                Name = "Semester 2021",
                Users = new List<User>()
            };
            Assignment newAssignment = new Assignment
            {
                Id = 1,
                Name = "Bug1",
                Project = projectTest,
                ProjectId = 1
            };
            Mock<IAssignmentLogic> mock = new Mock<IAssignmentLogic>(MockBehavior.Strict);
            Assignment receivedAssignment = null;
            mock.Setup(m => m.Add(It.IsAny<Assignment>())).Callback((Assignment sentBug) =>
            {
                receivedAssignment = sentBug;
            });
            AssignmentController controller = new AssignmentController(mock.Object);

            IActionResult result = controller.Post(newAssignment);

            mock.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(OkResult));
            CompareLogic compareLogic = new CompareLogic();
            ComparisonResult deepComparisonResult = compareLogic.Compare(newAssignment, receivedAssignment);
            Assert.IsTrue(deepComparisonResult.AreEqual);
        }

    }
}
