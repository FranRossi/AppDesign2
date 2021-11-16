using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using BusinessLogicInterface;
using CustomExceptions;
using Domain;
using Domain.DomainUtilities;
using KellermanSoftware.CompareNetObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestUtilities;
using WebApi.Models;

namespace WebApiTest
{

    [TestClass]
    [ExcludeFromCodeCoverage]
    public class BugModelTest
    {
        [TestMethod]
        public void ModelToEntity()
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
            Bug bug = bugToCompare.ToEntity();
            bug.Id = 1;
            CompareLogic compareLogic = new CompareLogic();
            ComparisonResult deepComparisonResult = compareLogic.Compare(expectedBug, bug);
            Assert.IsTrue(deepComparisonResult.AreEqual);
        }

        [TestMethod]
        public void InvalidModelToEntityNoId()
        {
            BugModel bugToCompare = new BugModel
            {
                Name = "Bug1",
                Description = "Bug en el servidor",
                Version = "1.4",
                State = BugState.Active,
                ProjectId = 1,
            };
            TestExceptionUtils.Throws<BugModelMissingFieldException>(
                () => bugToCompare.ToEntity(), "Missing Fields: Required -> Id, Name, Description, Version, State, ProjectId."
            );
        }

        [TestMethod]
        public void InvalidModelToEntityNoName()
        {
            BugModel bugToCompare = new BugModel
            {
                Id = 1,
                Description = "Bug en el servidor",
                Version = "1.4",
                State = BugState.Active,
                ProjectId = 1,
            };
            TestExceptionUtils.Throws<BugModelMissingFieldException>(
                () => bugToCompare.ToEntity(), "Missing Fields: Required -> Id, Name, Description, Version, State, ProjectId."
            );
        }

        [TestMethod]
        public void InvalidModelToEntityNoDescription()
        {
            BugModel bugToCompare = new BugModel
            {
                Id = 1,
                Name = "Bug1",
                Version = "1.4",
                State = BugState.Active,
                ProjectId = 1,
            };
            TestExceptionUtils.Throws<BugModelMissingFieldException>(
                () => bugToCompare.ToEntity(), "Missing Fields: Required -> Id, Name, Description, Version, State, ProjectId."
            );
        }

        [TestMethod]
        public void InvalidModelToEntityNoVersion()
        {
            BugModel bugToCompare = new BugModel
            {
                Id = 1,
                Name = "Bug1",
                Description = "Bug en el servidor",
                State = BugState.Active,
                ProjectId = 1,
            };
            TestExceptionUtils.Throws<BugModelMissingFieldException>(
                () => bugToCompare.ToEntity(), "Missing Fields: Required -> Id, Name, Description, Version, State, ProjectId."
            );
        }

        [TestMethod]
        public void InvalidModelToEntityNoState()
        {
            BugModel bugToCompare = new BugModel
            {
                Id = 1,
                Name = "Bug1",
                Description = "Bug en el servidor",
                Version = "1.4",
                ProjectId = 1,
            };
            TestExceptionUtils.Throws<BugModelMissingFieldException>(
                () => bugToCompare.ToEntity(), "Missing Fields: Required -> Id, Name, Description, Version, State, ProjectId."
            );
        }

        [TestMethod]
        public void InvalidModelToEntityNoProjectId()
        {
            BugModel bugToCompare = new BugModel
            {
                Id = 1,
                Name = "Bug1",
                Description = "Bug en el servidor",
                Version = "1.4",
                State = BugState.Active,
            };
            TestExceptionUtils.Throws<BugModelMissingFieldException>(
                () => bugToCompare.ToEntity(), "Missing Fields: Required -> Id, Name, Description, Version, State, ProjectId."
            );
        }

        [TestMethod]
        public void ModelToEntityWithId()
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
            Bug bug = bugToCompare.ToEntityWithID(expectedBug.Id);
            CompareLogic compareLogic = new CompareLogic();
            ComparisonResult deepComparisonResult = compareLogic.Compare(expectedBug, bug);
            Assert.IsTrue(deepComparisonResult.AreEqual);
        }

        [TestMethod]
        public void BugToModel()
        {
            Project newProject = new Project
            {
                Id = 1,
                Name = "Nuevo"
            };
            Bug expectedBug = new Bug
            {
                Id = 1,
                Name = "Bug1",
                Description = "Bug en el servidor",
                Version = "1.4",
                State = BugState.Active,
                ProjectId = 1,
                Project = newProject,
                Fixer = new User { UserName = "mario", Role = RoleType.Developer }
            };
            BugModel bugToCompare = new BugModel
            {
                Id = 1,
                Name = "Bug1",
                Description = "Bug en el servidor",
                Version = "1.4",
                State = BugState.Active,
                ProjectId = 1,
                ProjectName = "Nuevo",
                FixerName = "mario"
            };
            BugModel model = BugModel.ToModel(expectedBug);
            CompareLogic compareLogic = new CompareLogic();
            ComparisonResult deepComparisonResult = compareLogic.Compare(model, bugToCompare);
            Assert.IsTrue(deepComparisonResult.AreEqual);
        }

        [TestMethod]
        public void BugToModelList()
        {
            Project newProject = new Project
            {
                Id = 1,
                Name = "Nuevo"
            };
            Bug expectedBug = new Bug
            {
                Id = 1,
                Name = "Bug1",
                Description = "Bug en el servidor",
                Version = "1.4",
                State = BugState.Active,
                ProjectId = 1,
                Project = newProject
            };
            List<Bug> bugsToModel = new List<Bug>();
            bugsToModel.Add(expectedBug);
            BugModel bugModelToCompare = new BugModel
            {
                Id = 1,
                Name = "Bug1",
                Description = "Bug en el servidor",
                Version = "1.4",
                State = BugState.Active,
                ProjectId = 1,
                ProjectName = "Nuevo",
                FixerName = ""
            };
            IEnumerable<BugModel> models = new List<BugModel>();
            models = models.Append(bugModelToCompare);

            IEnumerable<BugModel> bugsConverted = BugModel.ToModelList(bugsToModel);
            CompareLogic compareLogic = new CompareLogic();
            ComparisonResult deepComparisonResult = compareLogic.Compare(models.First(), bugsConverted.First());
            Assert.IsTrue(deepComparisonResult.AreEqual);
        }

    }
}
