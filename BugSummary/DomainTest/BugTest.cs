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
                State = BugState.Fixed
            };
            Assert.AreEqual(BugState.Fixed, newBug.State);
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

        [TestMethod]
        public void CreateFixerDeveloperTest()
        {
            User user = new User
            {
                Role = RoleType.Developer
            };
            Bug newBug = new Bug
            {
                Fixer = user
            };
            Assert.IsNotNull(newBug.Fixer);
        }

        [DataRow(RoleType.Tester)]
        [DataRow(RoleType.Admin)]
        [DataTestMethod]
        public void CreateInvalidRoleFixerTest(RoleType role)
        {
            User user = new User
            {
                Role = role
            };
            TestExceptionUtils.Throws<InvalidBugSolverRoleException>(
                () => new Bug { Fixer = user }, "Bug fixers may only be Developers."
            );

        }

        [TestMethod]
        public void CreateFixerIdTest()
        {
            Bug newBug = new Bug
            {
                FixerId = 23
            };
            Assert.AreEqual(23, newBug.FixerId);
        }

        [TestMethod]
        public void CreateFixingTimeTest()
        {
            Bug newBug = new Bug
            {
                FixingTime = 23
            };
            Assert.AreEqual(23, newBug.FixingTime);
        }

        [TestMethod]
        public void CreateInvalidFixingTimeTest()
        {
            TestExceptionUtils.Throws<InvalidBugFixingTimeException>(
                () => new Bug { FixingTime = -2 }, "Bug fixing time should not be negative."
            );

        }

        [ExpectedException(typeof(BugNameLengthIncorrectException))]
        [TestMethod]
        public void VerifyBugNameLengthIsInCorrect()
        {
            string nameWithLengthOver60 = "Semester20Semester20Semester20Semester20Semester20Semester20PassingOver60";
            Bug newBug = new Bug
            {
                Name = nameWithLengthOver60
            };
        }


        [ExpectedException(typeof(BugIdLengthIncorrectException))]
        [TestMethod]
        public void VerifyBugIdLengthIsInCorrect()
        {
            Bug newBug = new Bug
            {
                Id = 12345
            };
        }

        [ExpectedException(typeof(BugDescriptionLengthIncorrectException))]
        [TestMethod]
        public void VerifyBugDescriptionLengthIsInCorrect()
        {
            Bug newBug = new Bug
            {
                Description = GenerateRandomStringWithSpecifiedLength(160)
            };
        }

        [ExpectedException(typeof(BugVersionLengthIncorrectException))]
        [TestMethod]
        public void VerifyBugVersionLengthIsInCorrect()
        {
            Bug newBug = new Bug
            {
                Version = GenerateRandomStringWithSpecifiedLength(11)
            };
        }

        private string GenerateRandomStringWithSpecifiedLength(int stringLength)
        {
            string descriptionOver150Characters = "";
            for (int i = 0; i < stringLength; i++)
            {
                descriptionOver150Characters += i;
            }
            return descriptionOver150Characters;
        }

        [ExpectedException(typeof(BugStateIncorrectException))]
        [TestMethod]
        public void VerifyBugStateIsInCorrect()
        {
            Bug newBug = new Bug
            {
                State = (BugState)(-1)
            };

        }

        [TestMethod]
        public void GetInvalidFixeHourlyRate()
        {
            Bug newBug = new Bug();

            Assert.AreEqual(0, newBug.GetFixerHourlyRate());
        }

        [TestMethod]
        public void GetFixeHourlyRate()
        {
            Bug newBug = new Bug
            {
                Fixer = new User
                {
                    Role = RoleType.Developer,
                    HourlyRate = 23
                }
            };

            Assert.AreEqual(23, newBug.GetFixerHourlyRate());
        }

    }
}