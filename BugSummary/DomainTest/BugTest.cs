using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain.DomainUtilities;
using Domain.DomainUtilities.CustomExceptions;

namespace Testing
{
    [TestClass]
    public class BugTest
    {
        [TestMethod]
        public void CreateId()
        {
            Bug newBug = new Bug
            {
                Id = 154
            };
            Assert.AreEqual(154, newBug.Id);
        }

        [TestMethod]
        public void CreateName()
        {
            Bug newBug = new Bug
            {
                Name = "Missing parenthesis"
            };
            Assert.AreEqual("Missing parenthesis", newBug.Name);
        }

        [TestMethod]
        public void CreateDescription()
        {
            Bug newBug = new Bug
            {
                Description = "On line 67, code won't compile because a parenthesis is missing"
            };
            Assert.AreEqual("On line 67, code won't compile because a parenthesis is missing", newBug.Description);
        }

        [TestMethod]
        public void CreateVersion()
        {
            Bug newBug = new Bug
            {
                Version = "1.0"
            };
            Assert.AreEqual("1.0", newBug.Version);
        }

        [TestMethod]
        public void CreateActiveState()
        {
            Bug newBug = new Bug
            {
                State = BugState.Active
            };
            Assert.AreEqual(BugState.Active, newBug.State);
        }

        [TestMethod]
        public void CreateInactiveState()
        {
            Bug newBug = new Bug
            {
                State = BugState.Inactive
            };
            Assert.AreEqual(BugState.Inactive, newBug.State);
        }

        [TestMethod]
        public void CreateProjectTest()
        {
            Bug newBug = new Bug
            {
                Project = new Project() { }
            };
            Assert.IsNotNull(newBug.Project);
        }

        [TestMethod]
        public void CreateProjectIdTest()
        {
            Bug newBug = new Bug
            {
                ProjectId = 2
            };
            Assert.IsNotNull(newBug.ProjectId);
        }

        [ExpectedException(typeof(BugNameLengthIncorrectException))]
        [TestMethod]
        public void VerifyBugNameLengthIsCorrect()
        {
            string nameWithLengthOver60 = "Semester20Semester20Semester20Semester20Semester20Semester20PassingOver60";
            Bug newBug = new Bug
            {
                Name = nameWithLengthOver60
            };
        }


        [ExpectedException(typeof(BugIdLengthIncorrectException))]
        [TestMethod]
        public void VerifyBugIdLengthIsCorrect()
        {
            Bug newBug = new Bug
            {
                Id = 12345
            };
        }

        [ExpectedException(typeof(BugDescriptionLengthIncorrectException))]
        [TestMethod]
        public void VerifyBugDescriptionLengthIsCorrect()
        {
            Bug newBug = new Bug
            {
                Description = GenerateRandomStringWithSpecifiedLength(160)
            };
        }

        [ExpectedException(typeof(BugVersionLengthIncorrectException))]
        [TestMethod]
        public void VerifyBugVersionLengthIsCorrect()
        {
            Bug newBug = new Bug
            {
                Version = GenerateRandomStringWithSpecifiedLength(11)
            };
        }

        private string GenerateRandomStringWithSpecifiedLength(int stringLength)
        {
            string descriptionOver150Characters = "";
            for (int i=0; i< stringLength; i++)
            {
                descriptionOver150Characters += i;
            }
            return descriptionOver150Characters;
        }
    }
}
