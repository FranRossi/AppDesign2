
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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
    public class BugReaderModelTest
    {
        [TestMethod]
        public void ParameterTest()
        {

            IEnumerable<Parameter> parameters = new List<Parameter> {
                new Parameter{
                    Name="Password",
                    Type = ParameterType.String
                },
                new Parameter{
                    Name="Path",
                    Type = ParameterType.String
                }
            };
            BugReaderModel model = new BugReaderModel
            {
                Parameters = parameters

            };

            CompareLogic compareLogic = new CompareLogic();
            ComparisonResult deepComparisonResult = compareLogic.Compare(parameters, model.Parameters);
            Assert.IsTrue(deepComparisonResult.AreEqual);
        }

        [TestMethod]
        public void PathTest()
        {

            string path = "somePath";
            BugReaderModel model = new BugReaderModel
            {
                FileName = path
            };

            Assert.AreEqual(path, model.FileName);
        }

    }
}
