using Domain;
using Domain.DomainUtilities;
using Domain.DomainUtilities.CustomExceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Testing
{
    [TestClass]
    public class BugTest
    {
        [TestMethod]
        public void CreateId()
        {
            var newBug = new Bug
            {
                Id = 154
            };
            Assert.AreEqual(154, newBug.Id);
        }

        [TestMethod]
        public void CreateName()
        {
            var newBug = new Bug
            {
                Name = "Missing parenthesis"
            };
            Assert.AreEqual("Missing parenthesis", newBug.Name);
        }

        [TestMethod]
        public void CreateDescription()
        {
            var newBug = new Bug
            {
                Description = "On line 67, code won't compile because a parenthesis is missing"
            };
            Assert.AreEqual("On line 67, code won't compile because a parenthesis is missing", newBug.Description);
        }

        [TestMethod]
        public void CreateVersion()
        {
            var newBug = new Bug
            {
                Version = "1.0"
            };
            Assert.AreEqual("1.0", newBug.Version);
        }

        [TestMethod]
        public void CreateActiveState()
        {
            var newBug = new Bug
            {
                State = BugState.Active
            };
            Assert.AreEqual(BugState.Active, newBug.State);
        }

        [TestMethod]
        public void CreateInactiveState()
        {
            var newBug = new Bug
            {
                State = BugState.Done
            };
            Assert.AreEqual(BugState.Done, newBug.State);
        }

        [TestMethod]
        public void CreateProjectTest()
        {
            var newBug = new Bug
            {
                Project = new Project()
            };
            Assert.IsNotNull(newBug.Project);
        }

        [TestMethod]
        public void CreateProjectIdTest()
        {
            var newBug = new Bug
            {
                ProjectId = 2
            };
            Assert.IsNotNull(newBug.ProjectId);
        }

        [ExpectedException(typeof(BugNameLengthIncorrectException))]
        [TestMethod]
        public void VerifyBugNameLengthIsInCorrect()
        {
            var nameWithLengthOver60 = "Semester20Semester20Semester20Semester20Semester20Semester20PassingOver60";
            var newBug = new Bug
            {
                Name = nameWithLengthOver60
            };
        }


        [ExpectedException(typeof(BugIdLengthIncorrectException))]
        [TestMethod]
        public void VerifyBugIdLengthIsInCorrect()
        {
            var newBug = new Bug
            {
                Id = 12345
            };
        }

        [ExpectedException(typeof(BugDescriptionLengthIncorrectException))]
        [TestMethod]
        public void VerifyBugDescriptionLengthIsInCorrect()
        {
            var newBug = new Bug
            {
                Description = GenerateRandomStringWithSpecifiedLength(160)
            };
        }

        [ExpectedException(typeof(BugVersionLengthIncorrectException))]
        [TestMethod]
        public void VerifyBugVersionLengthIsInCorrect()
        {
            var newBug = new Bug
            {
                Version = GenerateRandomStringWithSpecifiedLength(11)
            };
        }

        private string GenerateRandomStringWithSpecifiedLength(int stringLength)
        {
            var descriptionOver150Characters = "";
            for (var i = 0; i < stringLength; i++) descriptionOver150Characters += i;
            return descriptionOver150Characters;
        }

        [ExpectedException(typeof(BugStateIncorrectException))]
        [TestMethod]
        public void VerifyBugStateIsInCorrect()
        {
            var newBug = new Bug
            {
                State = (BugState) (-1)
            };
        }
    }
}