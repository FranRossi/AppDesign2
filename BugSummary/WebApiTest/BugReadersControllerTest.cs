using BusinessLogicInterface;
using Domain;
using ExternalReader;
using KellermanSoftware.CompareNetObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using WebApi.Controllers;
using WebApi.Models;

namespace WebApiTest
{
    [TestClass]
    public class BugReadersControllerTest
    {
        [TestMethod]
        public void GetBugReadersInfo()
        {
            IEnumerable<Parameter> parameters1 = new List<Parameter> {
                new Parameter{
                    Name="Path",
                    Type = ParameterType.String
                }
            };
            IEnumerable<Parameter> parameters2 = new List<Parameter> {
                new Parameter{
                    Name="Password",
                    Type = ParameterType.String
                }
            };
            Tuple<string, IEnumerable<Parameter>> tuple2 = Tuple.Create("Empresa1", parameters1);
            Tuple<string, IEnumerable<Parameter>> tuple1 = Tuple.Create("Empresa2", parameters2);
            IEnumerable<Tuple<string, IEnumerable<Parameter>>> bugReadersInfo = new List<Tuple<string, IEnumerable<Parameter>>>
            {
                tuple1,
                tuple2
            };
            IEnumerable<BugReaderInfoModel> expectedModel = BugReaderInfoModel.ToModel(bugReadersInfo);
            Mock<IProjectLogic> mock = new Mock<IProjectLogic>(MockBehavior.Strict);
            mock.Setup(r => r.GetExternalReadersInfo()).Returns(bugReadersInfo);
            BugReadersController controller = new BugReadersController(mock.Object);

            IActionResult result = controller.Get();
            OkObjectResult okResult = result as OkObjectResult;
            IEnumerable<BugReaderInfoModel> modelResult = okResult.Value as IEnumerable<BugReaderInfoModel>;

            mock.VerifyAll();
            Assert.AreEqual(200, okResult.StatusCode);
            CompareLogic compareLogic = new CompareLogic();
            ComparisonResult deepComparisonResult = compareLogic.Compare(expectedModel, modelResult);
            Assert.IsTrue(deepComparisonResult.AreEqual);
        }

    }
}