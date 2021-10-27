using Domain;
using Domain.DomainUtilities;
using Domain.DomainUtilities.CustomExceptions;
using TestUtilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;

namespace DomainTest
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class AssignmentTest
    {
        [TestMethod]
        public void CreateId()
        {
            Assignment newAssignment = new Assignment
            {
                Id = 154
            };
            Assert.AreEqual(154, newAssignment.Id);
        }

        [TestMethod]
        public void CreateName()
        {
            Assignment newAssignment = new Assignment
            {
                Name = "Monday's  Task"
            };
            Assert.AreEqual("Monday's  Task", newAssignment.Name);
        }

        [TestMethod]
        public void CreateHourlyRate()
        {
            Assignment newAssignment = new Assignment
            {
                HourlyRate = 15
            };
            Assert.AreEqual(15, newAssignment.HourlyRate);
        }

        [TestMethod]
        public void CreateDuration()
        {
            Assignment newAssignment = new Assignment
            {
                Duration = 1.5
            };
            Assert.AreEqual(1.5, newAssignment.Duration);
        }
        
        [TestMethod]
        public void CreateProject()
        {
            Assignment newAssignment = new Assignment
            {
                Project = new Project() { }
            };
            Assert.IsNotNull(newAssignment.Project);
        }
        
        [TestMethod]
        public void CreateProjectId()
        {
            Assignment newAssignment = new Assignment
            {
                ProjectId = 2
            };
            Assert.AreEqual(2, newAssignment.ProjectId);
        }
        
        
        [ExpectedException(typeof(AssignmentNameLengthIncorrectException))]
        [TestMethod]
        public void VerifyAssignmentNameLengthIsCorrect()
        {
            string name = "Semester2021Semester2021Semester2021Semester2021";
            Assignment newAssignment = new Assignment
            {
                Name = name
            };
        }
        
        [ExpectedException(typeof(AssignmentIdLengthIncorrectException))]
        [TestMethod]
        public void VerifyAssignmentIdLengthIsInCorrect()
        {
            Assignment newAssignment = new Assignment
            {
                Id = 12345
            };
        }
   
    }
}