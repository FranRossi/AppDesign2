using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using Domain.DomainUtilities;
using Utilities.Comparers;


namespace DomainTest
{

    [TestClass]
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
        public void CreateListofBugs()
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

    }
}
