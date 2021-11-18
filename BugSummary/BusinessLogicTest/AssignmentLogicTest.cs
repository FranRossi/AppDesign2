using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using BusinessLogic;
using DataAccess;
using DataAccessInterface;
using Domain;
using Domain.DomainUtilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TestUtilities.Comparers;

namespace BusinessLogicTest
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class AssignmentLogicTest
    {
        [TestMethod]
        public void AddBug()
        {
            Project projectTest = new Project()
            {
                Id = 1,
                Name = "Semester 2021",
                Users = new List<User>()
            };
            Assignment newAssignment = new Assignment
            {
                Id = 1,
                Name = "Bug1",
                Project = projectTest,
                ProjectId = 1
            };
            Assignment receivedAssignment = null;
            Mock<IAssignmentRepository> mockAssignmentRepository = new Mock<IAssignmentRepository>(MockBehavior.Strict);
            mockAssignmentRepository.Setup(mr => mr.Add(newAssignment)).Callback((Assignment assignment) =>
            {
                receivedAssignment = assignment;
            });
            mockAssignmentRepository.Setup(mr => mr.Save());

            AssignmentLogic assigmentLogic = new AssignmentLogic(mockAssignmentRepository.Object);
            assigmentLogic.Add(newAssignment);

            mockAssignmentRepository.VerifyAll();
            Assert.AreEqual(0, new AssignmentComparer().Compare(receivedAssignment, newAssignment));

        }
        
        
    }
}