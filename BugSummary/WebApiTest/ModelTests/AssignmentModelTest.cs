
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using CustomExceptions;
using Domain;
using KellermanSoftware.CompareNetObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestUtilities;
using WebApi.Models;

namespace WebApiTest
{

    [TestClass]
    [ExcludeFromCodeCoverage]
    public class AssignmentModelTest
    {
        [TestMethod]
        public void ModelToEntity()
        {
            Assignment assignment = new Assignment
            {
                Id = 1,
                Name = "Task",
                Duration = 2,
                HourlyRate = 25,
                ProjectId = 1
            };
            AssignmentModel assignmentModel = new AssignmentModel
            {
                Id = 1,
                Name = "Task",
                Duration = 2,
                HourlyRate = 25,
                ProjectId = 1,
            };
            Assignment assigmentTransformed = assignmentModel.ToEntity();
            assigmentTransformed.Id = 1;
            CompareLogic compareLogic = new CompareLogic();
            ComparisonResult deepComparisonResult = compareLogic.Compare(assignment, assigmentTransformed);
            Assert.IsTrue(deepComparisonResult.AreEqual);
        }

        [TestMethod]
        public void InvalidModelToEntityNoId()
        {
             AssignmentModel assignmentModel = new AssignmentModel
            {
                Name = "Task",
                Duration = 2,
                HourlyRate = 25,
                ProjectId = 1,
            };
            TestExceptionUtils.Throws<AssignmentModelMissingFieldException>(
                () => assignmentModel.ToEntity(), "Missing Fields: Required -> Id, Name, Duration, HourlyRate, ProjectId."
            );
        }

        [TestMethod]
        public void InvalidModelToEntityNoName()
        {
            AssignmentModel assignmentModel = new AssignmentModel
            {
                Id = 1,
                Duration = 2,
                HourlyRate = 25,
                ProjectId = 1,
            };
            TestExceptionUtils.Throws<AssignmentModelMissingFieldException>(
                () => assignmentModel.ToEntity(), "Missing Fields: Required -> Id, Name, Duration, HourlyRate, ProjectId."
            );
        }

        [TestMethod]
        public void InvalidModelToEntityNoDuration()
        {
            AssignmentModel assignmentModel = new AssignmentModel
            {
                Id = 1,
                Name = "Task",
                HourlyRate = 25,
                ProjectId = 1,
            };
            TestExceptionUtils.Throws<AssignmentModelMissingFieldException>(
                () => assignmentModel.ToEntity(), "Missing Fields: Required -> Id, Name, Duration, HourlyRate, ProjectId."
            );
        }

        [TestMethod]
        public void InvalidModelToEntityNoHourlyRate()
        {
            AssignmentModel assignmentModel = new AssignmentModel
            {
                Id = 1,
                Name = "Task",
                Duration = 2,
                ProjectId = 1,
            };
            TestExceptionUtils.Throws<AssignmentModelMissingFieldException>(
                () => assignmentModel.ToEntity(), "Missing Fields: Required -> Id, Name, Duration, HourlyRate, ProjectId."
            );
        }
        

        [TestMethod]
        public void InvalidModelToEntityNoProjectId()
        {
            AssignmentModel assignmentModel = new AssignmentModel
            {
                Id = 1,
                Name = "Task",
                Duration = 2,
                HourlyRate = 25
            };
            TestExceptionUtils.Throws<AssignmentModelMissingFieldException>(
                () => assignmentModel.ToEntity(), "Missing Fields: Required -> Id, Name, Duration, HourlyRate, ProjectId."
            );
        }
        
        [TestMethod]
        public void AssignmentToModel()
        {
            Assignment assignment = new Assignment
            {
                Id = 1,
                Name = "Task",
                Duration = 2,
                HourlyRate = 25,
                ProjectId = 1
            };
            AssignmentModel assignmentModel = new AssignmentModel
            {
                Id = 1,
                Name = "Task",
                Duration = 2,
                HourlyRate = 25,
                ProjectId = 1,
            };
            AssignmentModel model = AssignmentModel.ToModel(assignment);
            CompareLogic compareLogic = new CompareLogic();
            ComparisonResult deepComparisonResult = compareLogic.Compare(model, assignmentModel);
            Assert.IsTrue(deepComparisonResult.AreEqual);
        }
        
        [TestMethod]
        public void AssignmentToModelList()
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
            AssignmentModel assignmentModel = new AssignmentModel
            {
                Id = 1,
                Name = "Task",
                Duration = 2,
                HourlyRate = 25,
                ProjectId = 1,
            };
            IEnumerable<AssignmentModel> models = new List<AssignmentModel>();
            models = models.Append(assignmentModel);

            IEnumerable<AssignmentModel> assignmentsConverted = AssignmentModel.ToModelList(assignmentsToModel);
            CompareLogic compareLogic = new CompareLogic();
            ComparisonResult deepComparisonResult = compareLogic.Compare(models.First(), assignmentsConverted.First());
            Assert.IsTrue(deepComparisonResult.AreEqual);
        }

    }
}
