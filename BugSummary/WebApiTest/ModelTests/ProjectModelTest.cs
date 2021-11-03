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
    public class ProjectModelTest
    {

        [TestMethod]
        public void ProjectToEntityTest()
        {
            ProjectAddModel projectModel = new ProjectAddModel
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
            ProjectAddModel bugToCompare = new ProjectAddModel
            {
            };
            TestExceptionUtils.Throws<ProjectModelMissingFieldException>(
                () => bugToCompare.ToEntity(), "Missing Fields: Required -> Name."
            );
        }

        [TestMethod]
        public void ProjectModelToEntityTest()
        {
            IEnumerable<Project> projects = new List<Project>()
            {
                new Project
                    {
                        Id = 1,
                        Name = "Project A",
                        Bugs = new List<Bug> {
                            new Bug {
                                FixingTime = 3,
                                Fixer = new User
                                {
                                    Role = RoleType.Developer,
                                    HourlyRate = 2
                                }
                            }, new Bug(), new Bug(),
                        },
                        Users = new List<User>{new User(), new User(), new User() }
                    },
                new Project
                    {
                        Id = 2,
                        Name = "Project B",
                        Bugs = new List<Bug> { },
                        Users = new List<User>{ }
                    },
                new Project
                    {
                        Id = 3,
                        Name = "Project C",
                        Bugs = new List<Bug> {
                            new Bug {
                                FixingTime = 1,
                                Fixer = new User
                                {
                                    Role = RoleType.Developer,
                                    HourlyRate = 12
                                }
                            },
                            new Bug() },
                        Users = new List<User>{new User(), new User() }
                    }
            };
            IEnumerable<ProjectModel> expectedModel = new List<ProjectModel>()
            {
                new ProjectModel
                {
                    Id = 1,
                    Name = "Project A",
                    BugCount = 3,
                    Duration = 3,
                    Cost = 6,
                    Bugs = new List<BugModel> { new BugModel(), new BugModel(), new BugModel(), },
                    Users = new List<UserModel>{new UserModel(), new UserModel(), new UserModel()}
                },
                new ProjectModel
                {
                   Name = "Project B",
                   BugCount = 0,
                   Duration = 0,
                   Cost = 0,
                   Id = 2,
                   Bugs = new List<BugModel> {},
                   Users = new List<UserModel>{}
                },
                new ProjectModel
                {
                    Id = 3,
                    Name = "Project C",
                    BugCount = 2,
                    Duration = 1,
                    Cost = 12,
                    Bugs = new List<BugModel> { new BugModel(), new BugModel() },
                    Users = new List<UserModel>{new UserModel(), new UserModel()}
                }
            };

            IEnumerable<ProjectModel> model = ProjectModel.ToModel(projects);
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
                        Bugs = new List<Bug> { new Bug(), new Bug(), new Bug() },
                        Users = new List<User> {}
                    }
            };
            IEnumerable<ProjectModel> expectedModel = new List<ProjectModel>()
            {
                new ProjectModel
                {
                    Name = "Project A",
                    BugCount = 3,
                    Bugs = new List<BugModel> { new BugModel(), new BugModel(), new BugModel() },
                    Users = new List<UserModel> {}
                }
            };

            IEnumerable<ProjectModel> model = ProjectModel.ToModel(projects);
            CompareLogic compareLogic = new CompareLogic();
            ComparisonResult deepComparisonResult = compareLogic.Compare(expectedModel, model);
            Assert.IsTrue(deepComparisonResult.AreEqual);
        }
    }
}
