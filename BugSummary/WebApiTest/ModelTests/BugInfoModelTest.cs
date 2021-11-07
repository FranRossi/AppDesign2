
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using CustomExceptions;
using Domain;
using ExternalReader;
using KellermanSoftware.CompareNetObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestUtilities;
using WebApi.Models;

namespace WebApiTest
{

    [TestClass]
    [ExcludeFromCodeCoverage]
    public class BugInfoModelTest
    {
        [TestMethod]
        public void ToModelTestTwoTuple()
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
            IEnumerable<BugInfoModel> expectedModel = new List<BugInfoModel>()
            {
                new BugInfoModel
                {
                    Name = "Empresa2",
                    Parameters = parameters2,
                },
                new BugInfoModel
                {
                    Name = "Empresa1",
                    Parameters = parameters1,
                }
            };

            IEnumerable<BugInfoModel> model = BugInfoModel.ToModel(bugReadersInfo);
            CompareLogic compareLogic = new CompareLogic();
            ComparisonResult deepComparisonResult = compareLogic.Compare(expectedModel, model);
            Assert.IsTrue(deepComparisonResult.AreEqual);
        }

        [TestMethod]
        public void ToModelTestOneTuple()
        {
            IEnumerable<Parameter> parameters1 = new List<Parameter> {
                new Parameter{
                    Name="Path",
                    Type = ParameterType.String
                }
            };
            Tuple<string, IEnumerable<Parameter>> tuple2 = Tuple.Create("Empresa1", parameters1);
            IEnumerable<Tuple<string, IEnumerable<Parameter>>> bugReadersInfo = new List<Tuple<string, IEnumerable<Parameter>>>
            {
                tuple2
            };
            IEnumerable<BugInfoModel> expectedModel = new List<BugInfoModel>()
            {
                new BugInfoModel
                {
                    Name = "Empresa1",
                    Parameters = parameters1,
                }
            };

            IEnumerable<BugInfoModel> model = BugInfoModel.ToModel(bugReadersInfo);
            CompareLogic compareLogic = new CompareLogic();
            ComparisonResult deepComparisonResult = compareLogic.Compare(expectedModel, model);
            Assert.IsTrue(deepComparisonResult.AreEqual);
        }

        [TestMethod]
        public void ToModelTestEmpty()
        {
            IEnumerable<Tuple<string, IEnumerable<Parameter>>> bugReadersInfo = new List<Tuple<string, IEnumerable<Parameter>>>();
            IEnumerable<BugInfoModel> expectedModel = new List<BugInfoModel>();

            IEnumerable<BugInfoModel> model = BugInfoModel.ToModel(bugReadersInfo);
            CompareLogic compareLogic = new CompareLogic();
            ComparisonResult deepComparisonResult = compareLogic.Compare(expectedModel, model);
            Assert.IsTrue(deepComparisonResult.AreEqual);
        }

    }
}
