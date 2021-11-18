
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Domain;
using Domain.DomainUtilities;
using KellermanSoftware.CompareNetObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestUtilities;
using Utilities.CustomExceptions.WebApi;
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
                    BugCount = 3
                },
                new ProjectModel
                {
                   Name = "Project B",
                   BugCount = 0,
                   Id = 2,
                },
                new ProjectModel
                {
                    Id = 3,
                    Name = "Project C",
                    BugCount = 2,
                }
            };

            IEnumerable<ProjectModel> model = ProjectModel.ToModelList(projects);
            CompareLogic compareLogic = new CompareLogic();
            ComparisonResult deepComparisonResult = compareLogic.Compare(expectedModel, model);
            Assert.IsTrue(deepComparisonResult.AreEqual);
        }

        [TestMethod]
        public void ProjectZeroBugCountToModelTest()
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
                    BugCount = 3
                }
            };

            IEnumerable<ProjectModel> model = ProjectModel.ToModelList(projects);
            CompareLogic compareLogic = new CompareLogic();
            ComparisonResult deepComparisonResult = compareLogic.Compare(expectedModel, model);
            Assert.IsTrue(deepComparisonResult.AreEqual);
        }
        
        [TestMethod]
        public void ProjectWithAssignmentsToModel()
        {
            Assignment assignment = new Assignment
            {
                Id = 1,
                Name = "Task",
                Duration = 2,
                HourlyRate = 25,
                ProjectId = 1
            };
            List<Assignment> assignmentsToModel = new List<Assignment>();
            assignmentsToModel.Add(assignment);

            IEnumerable<AssignmentModel> assignmentsConverted = AssignmentModel.ToModelList(assignmentsToModel);
            
            Project project = new Project
           {
               Name = "Project A",
               Bugs = new List<Bug>{},
               Users = new List<User> { },
               Assignments = assignmentsToModel,
           };
            ProjectModel expectedModel = new ProjectModel
            {
                Name = "Project A",
                BugCount = 0,
                Bugs = new List<BugModel>{},
                Users = new List<UserModel>{},
                Assignments = assignmentsConverted,
                Cost = 50,
                Duration = 2
            };

            ProjectModel model = ProjectModel.ToModel(project);
            CompareLogic compareLogic = new CompareLogic();
            ComparisonResult deepComparisonResult = compareLogic.Compare(expectedModel, model);
            Assert.IsTrue(deepComparisonResult.AreEqual);
        }
        
    }
}
