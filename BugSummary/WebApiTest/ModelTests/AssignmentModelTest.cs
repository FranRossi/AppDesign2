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
    public class AssignmentModelTest
    {
        [TestMethod]
        public void ModelToEntity()
        {
            Assignment assignment = new Assignment
            {
                Id = 1,
                Name = "Bug1",
               Duration = 2,
               HourlyRate = 25,
               ProjectId = 1,
            };
            AssignmentModel assignmentModel = new AssignmentModel
            {
                Id = 1,
                Name = "Bug1",
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
                Name = "Bug1",
                Duration = 2,
                HourlyRate = 25,
                ProjectId = 1,
            };
            TestExceptionUtils.Throws<AssignmentModelMissingFieldException>(
                () => assignmentModel.ToEntity(), "Missing Fields: Required -> Id, Name, Duration, Hourly, ProjectId."
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
                () => assignmentModel.ToEntity(), "Missing Fields: Required -> Id, Name, Duration, Hourly, ProjectId."
            );
        }

        [TestMethod]
        public void InvalidModelToEntityNoDuration()
        {
            AssignmentModel assignmentModel = new AssignmentModel
            {
                Id = 1,
                Name = "Bug1",
                HourlyRate = 25,
                ProjectId = 1,
            };
            TestExceptionUtils.Throws<AssignmentModelMissingFieldException>(
                () => assignmentModel.ToEntity(), "Missing Fields: Required -> Id, Name, Duration, Hourly, ProjectId."
            );
        }

        [TestMethod]
        public void InvalidModelToEntityNoHourlyRate()
        {
            AssignmentModel assignmentModel = new AssignmentModel
            {
                Id = 1,
                Name = "Bug1",
                Duration = 2,
                ProjectId = 1,
            };
            TestExceptionUtils.Throws<AssignmentModelMissingFieldException>(
                () => assignmentModel.ToEntity(), "Missing Fields: Required -> Id, Name, Duration, Hourly, ProjectId."
            );
        }
        

        [TestMethod]
        public void InvalidModelToEntityNoProjectId()
        {
            AssignmentModel assignmentModel = new AssignmentModel
            {
                Id = 1,
                Name = "Bug1",
                Duration = 2,
                HourlyRate = 25
            };
            TestExceptionUtils.Throws<AssignmentModelMissingFieldException>(
                () => assignmentModel.ToEntity(), "Missing Fields: Required -> Id, Name, Duration, Hourly, ProjectId."
            );
        }
        

    }
}
