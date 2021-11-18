using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Domain;
using Domain.DomainUtilities;
using Domain.DomainUtilities.CustomExceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestUtilities;
using TestUtilities.Comparers;

namespace DomainTest
{

    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ProjectTest
    {
        [TestMethod]
        public void CreateId()
        {
            Project newProject = new Project
            {
                Id = 154
            };
            Assert.AreEqual(154, newProject.Id);
        }

        [TestMethod]
        public void CreateName()
        {
            Project newProject = new Project
            {
                Name = "Semester2021"
            };
            Assert.AreEqual("Semester2021", newProject.Name);
        }

        [TestMethod]
        public void CreateListOfBugs()
        {
            Project newProject = new Project
            {
                Bugs = new List<Bug>()
                {
                    new Bug
                    {
                        Id = 1,
                        Name = "Bug1",
                        Description = "Bug en el servidor",
                        Version = "1.4",
                        State = BugState.Active,
                        Project = new Project() { }
                    }
                }
            };

            Assert.AreEqual(1, newProject.Bugs.Count());
        }

        [TestMethod]
        public void CreateListOfAssignments()
        {
            Project newProject = new Project
            {
                Assignments = new List<Assignment>()
                {
                    new Assignment
                    {
                        Id = 1,
                        Name = "Bug1",
                        Duration = 2,
                        HourlyRate = 25,
                        Project = new Project() { }
                    }
                }
            };

            Assert.AreEqual(1, newProject.Assignments.Count());
        }

        [TestMethod]
        public void AddMultipleTasks()
        {
            List<Assignment> assignmentsExpected = new List<Assignment>()
            {
                new Assignment
                {
                    Id = 1,
                    Name = "Ass1",
                    Project = new Project() { }
                },
                new Assignment
                {
                    Id = 2,
                    Name = "Ass2",
                    Project = new Project() { }
                }
            };
            Project newProject = new Project
            {
                Assignments = new List<Assignment>()
                {
                    new Assignment
                    {
                        Id = 1,
                        Name = "Ass1",
                        Project = new Project() { }
                    },
                    new Assignment
                    {
                        Id = 2,
                        Name = "Ass2",
                        Project = new Project() { }
                    }
                }
            };
            Assert.AreEqual(2, newProject.Assignments.Count());
            CollectionAssert.AreEqual(assignmentsExpected, newProject.Assignments, new AssignmentComparer());
        }

        [TestMethod]
        public void AddMultipleBugs()
        {
            List<Bug> bugsExpected = new List<Bug>()
            {
                new Bug
                    {
                        Id = 1,
                        Name = "Bug1",
                        Description = "Bug en el servidor",
                        Version = "1.4",
                        State = BugState.Active,
                        Project = new Project() { }
                    },
                    new Bug
                    {
                        Id = 2,
                        Name = "Bug2",
                        Description = "Bug en el cliente",
                        Version = "1.4",
                        State = BugState.Active,
                        Project = new Project() { }
                    }
            };
            Project newProject = new Project
            {
                Bugs = new List<Bug>()
                {
                    new Bug
                    {
                        Id = 1,
                        Name = "Bug1",
                        Description = "Bug en el servidor",
                        Version = "1.4",
                        State = BugState.Active,
                        Project = new Project() { }
                    },
                    new Bug
                    {
                        Id = 2,
                        Name = "Bug2",
                        Description = "Bug en el cliente",
                        Version = "1.4",
                        State = BugState.Active,
                        Project = new Project() { }
                    }
                }
            };
            Assert.AreEqual(2, newProject.Bugs.Count());
            CollectionAssert.AreEqual(bugsExpected, newProject.Bugs, new BugComparer());
        }

        [TestMethod]
        public void CreateListofTestersAndDevelopers()
        {
            Project newProject = new Project
            {
                Users = new List<User>()
                {
                    new User
                    {
                        Id = 1,
                        FirstName = "Pepe",
                        LastName = "Perez",
                        Password = "pepe1234",
                        UserName = "pp",
                        Email = "pepe@gmail.com",
                        Role = RoleType.Tester,
                        Projects = new List<Project>()
                    },
                    new User
                    {
                        Id = 2,
                        FirstName = "Juan",
                        LastName = "Perez",
                        Password = "pepe1234",
                        UserName = "pp",
                        Email = "Juan@gmail.com",
                        Role = RoleType.Developer,
                        Projects = new List<Project>()
                    }
                }
            };

            Assert.AreEqual(2, newProject.Users.Count());
        }

        [TestMethod]
        public void AddTester()
        {
            Project newProject = new Project();
            User newUser = new User
            {
                Id = 1,
                FirstName = "Pepe",
                LastName = "Perez",
                Password = "pepe1234",
                UserName = "pp",
                Email = "pepe@gmail.com",
                Role = RoleType.Tester,
                Projects = new List<Project>()
            };
            newProject.AddUser(newUser);

            Assert.AreEqual(newUser, newProject.Users.ElementAt(0));
        }

        [TestMethod]
        public void AddDeveloper()
        {
            Project newProject = new Project();
            User newUser = new User
            {
                Id = 1,
                FirstName = "Pepe",
                LastName = "Perez",
                Password = "pepe1234",
                UserName = "pp",
                Email = "pepe@gmail.com",
                Role = RoleType.Developer,
                Projects = new List<Project>()
            };
            newProject.AddUser(newUser);

            Assert.AreEqual(newUser, newProject.Users.ElementAt(0));
        }

        [TestMethod]
        public void AddInvalidRoleAsignee()
        {
            Project newProject = new Project();
            User newUser = new User
            {
                Id = 1,
                FirstName = "Pepe",
                LastName = "Perez",
                Password = "pepe1234",
                UserName = "pp",
                Email = "pepe@gmail.com",
                Role = RoleType.Admin,
                Projects = new List<Project>()
            };


            TestExceptionUtils.Throws<InvalidProjectAssigneeRoleException>(
               () => newProject.AddUser(newUser), "Project asingnees must either be Developers or Testers."
            );
        }

        [DataRow(RoleType.Admin)]
        [DataRow(RoleType.Developer)]
        [DataRow(RoleType.Tester)]
        [DataTestMethod]
        public void RemoveUser(RoleType role)
        {
            Project newProject = new Project
            {
                Name = "Semester2021"
            };
            User newUser = new User
            {
                Id = 1,
                FirstName = "Pepe",
                LastName = "Perez",
                Password = "pepe1234",
                UserName = "pp",
                Email = "pepe@gmail.com",
                Role = role,
                Projects = new List<Project>()
            };
            newProject.Users = new List<User> { newUser };
            newProject.RemoveUser(newUser);

            Assert.AreEqual(0, newProject.Users.Count);
        }

        [ExpectedException(typeof(ProjectNameLengthIncorrectException))]
        [TestMethod]
        public void VerifyProjectNameLengthIsCorrect()
        {
            string name = "Semester2021Semester2021Semester2021Semester2021";
            Project newProject = new Project
            {
                Name = name
            };
        }

        [TestMethod]
        public void DurationEmptyProject()
        {
            Project newProject = new Project();
            Assert.AreEqual(0, newProject.CalculateDuration());
        }

        [TestMethod]
        public void DurationProject()
        {
            Project newProject = new Project
            {
                Bugs = new List<Bug>()
                {
                    new Bug
                    {
                        Id = 1,
                        Name = "Bug1",
                        Description = "Bug en el servidor",
                        Version = "1.4",
                        State = BugState.Fixed,
                        FixingTime = 12,
                        Project = new Project() { }
                    },
                    new Bug
                    {
                        Id = 2,
                        Name = "Bug2",
                        Description = "Bug en el cliente",
                        Version = "1.4",
                        State = BugState.Fixed,
                        FixingTime = 3,
                        Project = new Project() { }
                    }
                },
                Assignments = new List<Assignment>()
                {
                    new Assignment
                    {
                        Id = 1,
                        Name = "Ass1",
                        Project = new Project() { },
                        Duration = 1
                    },
                    new Assignment
                    {
                        Id = 2,
                        Name = "Ass2",
                        Project = new Project() { },
                        Duration = 6
                    }
                }
            };
            Assert.AreEqual(22, newProject.CalculateDuration());
        }

        [TestMethod]
        public void EmptyProjectCost()
        {
            Project newProject = new Project();
            Assert.AreEqual(0, newProject.CalculateCost());
        }

        [TestMethod]
        public void ProjectCost()
        {
            Project newProject = new Project
            {
                Bugs = new List<Bug>()
                {
                    new Bug
                    {
                        Id = 1,
                        Name = "Bug1",
                        Description = "Bug en el servidor",
                        Version = "1.4",
                        State = BugState.Fixed,
                        FixingTime = 12,
                        Fixer = new User() {
                            Role = RoleType.Developer,
                            HourlyRate = 2
                        }
                    },
                    new Bug
                    {
                        Id = 2,
                        Name = "Bug2",
                        Description = "Bug en el cliente",
                        Version = "1.4",
                        State = BugState.Fixed,
                        FixingTime = 3,
                        Fixer = new User() {
                            Role = RoleType.Developer,
                            HourlyRate = 4
                        }
                    }
                },
                Assignments = new List<Assignment>()
                {
                    new Assignment
                    {
                        Id = 1,
                        Name = "Ass1",
                        Project = new Project() { },
                        Duration = 1,
                        HourlyRate = 4
                    },
                    new Assignment
                    {
                        Id = 2,
                        Name = "Ass2",
                        Project = new Project() { },
                        Duration = 6,
                        HourlyRate = 3
                    }
                }
            };
            Assert.AreEqual(58, newProject.CalculateCost());
        }

    }
}
