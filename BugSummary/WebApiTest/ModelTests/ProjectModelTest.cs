using System.Collections;
using System.Collections.Generic;
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
    public class ProjectModelTest
    {

        [TestMethod]
        public void ProjectToEntityTest()
        {
            ProjectModel projectModel = new ProjectModel
            {
                Name = "New Project 2022"
            };
            Project ExpectedProject = new Project
            {
                Name = "New Project 2022"
            };

            Project result = projectModel.ToEntity();
            CompareLogic compareLogic = new CompareLogic();
            ComparisonResult deepComparisonResult = compareLogic.Compare(ExpectedProject, result);
            Assert.IsTrue(deepComparisonResult.AreEqual);
        }

        [TestMethod]
        public void InvalidModelToEntityNoId()
        {
            ProjectModel bugToCompare = new ProjectModel
            {
            };
            TestExceptionUtils.Throws<ProjectModelMissingFieldException>(
                () => bugToCompare.ToEntity(), "Missing Fields: Required -> Name."
            );
        }

        public void ProjectBugCountToEntityTest()
        {
            IEnumerable<Project> projects = new List<Project>()
            {
                new Project
                    {
                        Name = "Project A",
                        Bugs = new List<Bug> { new Bug(), new Bug(), new Bug(), }
                    },
                new Project
                    {
                        Name = "Project B",
                        Bugs = new List<Bug> {  }
                    },
                new Project
                    {
                        Name = "Project C",
                        Bugs = new List<Bug> { new Bug(), new Bug() }
                    }
            };
            IEnumerable<ProjectBugCountModel> expectedModel = new List<ProjectBugCountModel>()
            {
                new ProjectBugCountModel
                {
                    Name = "Project A",
                    BugCount = 3
                },
                new ProjectBugCountModel
                {
                        Name = "Project B",
                        BugCount = 0
                },
                new ProjectBugCountModel
                {
                    Name = "Project C",
                    BugCount = 2
                }
            };

            IEnumerable<ProjectBugCountModel> model = ProjectBugCountModel.ToModel(projects);
            CompareLogic compareLogic = new CompareLogic();
            ComparisonResult deepComparisonResult = compareLogic.Compare(expectedModel, model);
            Assert.IsTrue(deepComparisonResult.AreEqual);
        }

        [TestMethod]
        public void ProjectZeroBugCountToEntityTest()
        {
            IEnumerable<Project> projects = new List<Project>()
            {
                new Project
                    {
                        Name = "Project A",
                        Bugs = new List<Bug> { new Bug(), new Bug(), new Bug(), }
                    }
            };
            IEnumerable<ProjectBugCountModel> expectedModel = new List<ProjectBugCountModel>()
            {
                new ProjectBugCountModel
                {
                    Name = "Project A",
                    BugCount = 3
                }
            };

            IEnumerable<ProjectBugCountModel> model = ProjectBugCountModel.ToModel(projects);
            CompareLogic compareLogic = new CompareLogic();
            ComparisonResult deepComparisonResult = compareLogic.Compare(expectedModel, model);
            Assert.IsTrue(deepComparisonResult.AreEqual);
        }
    }
}
