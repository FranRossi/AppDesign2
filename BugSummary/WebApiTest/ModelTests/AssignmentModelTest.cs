
using System.Diagnostics.CodeAnalysis;
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
        

    }
}
