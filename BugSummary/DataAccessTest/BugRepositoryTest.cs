using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using DataAccess;
using DataAccess.Exceptions;
using Domain;
using Domain.DomainUtilities;
using KellermanSoftware.CompareNetObjects;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestUtilities;
using Utilities.Comparers;

namespace DataAccessTest
{
    [TestClass]
    public class BugRepositoryTest
    {
        private readonly BugRepository _bugRepository;
        private readonly BugSummaryContext _bugSummaryContext;

        private readonly DbConnection _connection;
        private readonly DbContextOptions<BugSummaryContext> _contextOptions;

        public BugRepositoryTest()
        {
            _connection = new SqliteConnection("Filename=:memory:");
            _contextOptions = new DbContextOptionsBuilder<BugSummaryContext>().UseSqlite(_connection).Options;
            _bugSummaryContext = new BugSummaryContext(_contextOptions);
            _bugRepository = new BugRepository(_bugSummaryContext);
        }

        [TestInitialize]
        public void Setup()
        {
            _connection.Open();
            _bugSummaryContext.Database.EnsureCreated();
        }

        [TestCleanup]
        public void CleanUp()
        {
            _bugSummaryContext.Database.EnsureDeleted();
        }

        [TestMethod]
        public void AddNewBugTest()
        {
            var newBugToAdd = new Bug
            {
                Id = 1,
                Name = "Bug1",
                Description = "Bug en el servidor",
                Version = "1.4",
                State = BugState.Active,
                Project = new Project(),
                ProjectId = 1
            };
            _bugRepository.Add(newBugToAdd);
            _bugRepository.Save();

            var bugsExpected = new List<Bug>();
            bugsExpected.Add(newBugToAdd);

            using (var context = new BugSummaryContext(_contextOptions))
            {
                var bugsDataBase = context.Bugs.ToList();
                Assert.AreEqual(1, bugsDataBase.Count());
                CollectionAssert.AreEqual(bugsExpected, bugsDataBase, new BugComparer());
            }
        }

        [TestMethod]
        public void TesterAddsBugWithoutNewProjectTest()
        {
            var testerUser = new User
            {
                Id = 2,
                FirstName = "Juan",
                LastName = "Rodriguez",
                Password = "pepe1234",
                UserName = "pp",
                Email = "pepe@gmail.com",
                Role = RoleType.Tester,
                Projects = new List<Project>()
            };
            using (var context = new BugSummaryContext(_contextOptions))
            {
                var projectTester = new Project
                {
                    Id = 1,
                    Name = "Semester 2021",
                    Users = new List<User>
                    {
                        testerUser
                    }
                };
                var projectTester2 = new Project
                {
                    Id = 2,
                    Name = "Semester 2021",
                    Users = new List<User>()
                };
                context.Projects.Add(projectTester);
                testerUser.Projects.Add(projectTester);
                //User does not have second project
                context.Projects.Add(projectTester2);
                var oldBug = new Bug
                {
                    Id = 1,
                    Name = "Bug1",
                    Description = "Bug en el servidor",
                    Version = "1.4",
                    State = BugState.Active,
                    ProjectId = 1
                };
                context.Add(oldBug);
                context.SaveChanges();
            }

            var updatedBug = new Bug
            {
                Id = 1,
                Name = "BugNuevo",
                Description = "Bug Nuevo",
                Version = "1.5",
                State = BugState.Done,
                ProjectId = 2
            };

            TestExceptionUtils.Throws<ProjectDontBelongToUser>(
                () => _bugRepository.Add(testerUser, updatedBug), "New project to update bug, does not belong to tester"
            );
        }

        [TestMethod]
        public void GetAllBugsFromRepositoryTest()
        {
            using (var context = new BugSummaryContext(_contextOptions))
            {
                context.Add(new Bug
                {
                    Id = 1,
                    Name = "Bug1",
                    Description = "Bug en el servidor",
                    Version = "1.4",
                    State = BugState.Active,
                    Project = new Project(),
                    ProjectId = 1
                });
                context.SaveChanges();
                context.Add(new Bug
                {
                    Id = 2,
                    Name = "Bug2",
                    Description = "Bug en el cliente",
                    Version = "1.4",
                    State = BugState.Active,
                    Project = new Project(),
                    ProjectId = 1
                });
                context.SaveChanges();
            }

            var newBug1 = new Bug
            {
                Id = 1,
                Name = "Bug1",
                Description = "Bug en el servidor",
                Version = "1.4",
                State = BugState.Active,
                Project = new Project(),
                ProjectId = 1
            };
            var newBug2 = new Bug
            {
                Id = 2,
                Name = "Bug2",
                Description = "Bug en el cliente",
                Version = "1.4",
                State = BugState.Active,
                Project = new Project(),
                ProjectId = 1
            };
            var bugsExpected = new List<Bug>();
            bugsExpected.Add(newBug1);
            bugsExpected.Add(newBug2);


            using (var context = new BugSummaryContext(_contextOptions))
            {
                var bugsDataBase = context.Bugs.ToList();
                Assert.AreEqual(2, bugsDataBase.Count());
                CollectionAssert.AreEqual(bugsExpected, bugsDataBase, new BugComparer());
            }
        }

        [TestMethod]
        public void GetAllBugsFromTester()
        {
            var testerUser = new User
            {
                Id = 2,
                FirstName = "Juan",
                LastName = "Rodriguez",
                Password = "pepe1234",
                UserName = "pp",
                Email = "pepe@gmail.com",
                Role = RoleType.Tester,
                Projects = new List<Project>()
            };
            var projectTester = new Project
            {
                Id = 1,
                Name = "Semester 2021",
                Users = new List<User>
                {
                    testerUser
                }
            };
            testerUser.Projects.Add(projectTester);
            using (var context = new BugSummaryContext(_contextOptions))
            {
                context.Add(new Bug
                {
                    Id = 1,
                    Name = "Bug1",
                    Description = "Bug en el servidor",
                    Version = "1.4",
                    State = BugState.Active,
                    Project = projectTester,
                    ProjectId = 1
                });
                context.SaveChanges();
                context.Add(new Bug
                {
                    Id = 2,
                    Name = "Bug2",
                    Description = "Bug en el cliente",
                    Version = "1.4",
                    State = BugState.Active,
                    Project = new Project(),
                    ProjectId = 1
                });
                context.SaveChanges();
            }

            var bugsExpected = new List<Bug>
            {
                new()
                {
                    Id = 1,
                    Name = "Bug1",
                    Description = "Bug en el servidor",
                    Version = "1.4",
                    State = BugState.Active,
                    Project = projectTester,
                    ProjectId = 1
                }
            };

            var bugsDataBase = _bugRepository.GetAllByTester(testerUser).ToList();

            using (var context = new BugSummaryContext(_contextOptions))
            {
                Assert.AreEqual(1, bugsDataBase.Count());
                CollectionAssert.AreEqual(bugsExpected, bugsDataBase, new BugComparer());
            }
        }

        [TestMethod]
        [ExpectedException(typeof(UserMustBeTesterException))]
        public void DeveloperCreatesBug()
        {
            var developerUser = new User
            {
                Id = 2,
                FirstName = "Juan",
                LastName = "Rodriguez",
                Password = "pepe1234",
                UserName = "pp",
                Email = "pepe@gmail.com",
                Role = RoleType.Developer,
                Projects = new List<Project>()
            };
            var newBug = new Bug
            {
                Id = 1,
                Name = "Bug1",
                Description = "Bug en el servidor",
                Version = "1.4",
                State = BugState.Active,
                Project = new Project(),
                ProjectId = 1
            };
            _bugRepository.Add(developerUser, newBug);
        }

        [TestMethod]
        public void TesterUpdateBugTest()
        {
            var testerUser = new User
            {
                Id = 2,
                FirstName = "Juan",
                LastName = "Rodriguez",
                Password = "pepe1234",
                UserName = "pp",
                Email = "pepe@gmail.com",
                Role = RoleType.Tester,
                Projects = new List<Project>()
            };
            using (var context = new BugSummaryContext(_contextOptions))
            {
                var projectTester = new Project
                {
                    Id = 1,
                    Name = "Semester 2021",
                    Users = new List<User>
                    {
                        testerUser
                    }
                };
                var projectTester2 = new Project
                {
                    Id = 2,
                    Name = "Semester 2021",
                    Users = new List<User>
                    {
                        testerUser
                    }
                };
                context.Projects.Add(projectTester);
                testerUser.Projects.Add(projectTester);
                context.Projects.Add(projectTester2);
                testerUser.Projects.Add(projectTester2);
                var oldBug = new Bug
                {
                    Id = 1,
                    Name = "Bug1",
                    Description = "Bug en el servidor",
                    Version = "1.4",
                    State = BugState.Active,
                    ProjectId = 1
                };
                context.Add(oldBug);
                context.SaveChanges();
            }

            var updatedBug = new Bug
            {
                Id = 1,
                Name = "BugNuevo",
                Description = "Bug Nuevo",
                Version = "1.5",
                State = BugState.Done,
                ProjectId = 2
            };
            _bugRepository.Update(testerUser, updatedBug);
            _bugRepository.Save();

            using (var context = new BugSummaryContext(_contextOptions))
            {
                var databaseBug = context.Bugs.ToList().First(u => u.Id == updatedBug.Id);
                var compareLogic = new CompareLogic();
                var deepComparisonResult = compareLogic.Compare(updatedBug, databaseBug);
                Assert.IsTrue(deepComparisonResult.AreEqual);
            }
        }

        [TestMethod]
        public void UpdateInexistentBugTest()
        {
            var testerUser = new User
            {
                Id = 2,
                FirstName = "Juan",
                LastName = "Rodriguez",
                Password = "pepe1234",
                UserName = "pp",
                Email = "pepe@gmail.com",
                Role = RoleType.Tester,
                Projects = new List<Project>()
            };
            var projectTester = new Project
            {
                Id = 1,
                Name = "Semester 2021",
                Users = new List<User>
                {
                    testerUser
                }
            };
            testerUser.Projects.Add(projectTester);
            var updatedBug = new Bug
            {
                Id = 1,
                Name = "BugNuevo",
                Description = "Bug en el cliente",
                Version = "1.5",
                State = BugState.Done,
                ProjectId = 1
            };
            TestExceptionUtils.Throws<InexistentBugException>(
                () => _bugRepository.Update(testerUser, updatedBug), "The entered bug does not exist."
            );
        }

        [TestMethod]
        public void DeveloperUpdateBugTest()
        {
            var developerUser = new User
            {
                Id = 2,
                FirstName = "Juan",
                LastName = "Rodriguez",
                Password = "pepe1234",
                UserName = "pp",
                Email = "pepe@gmail.com",
                Role = RoleType.Developer,
                Projects = new List<Project>()
            };
            var projectTester = new Project
            {
                Id = 1,
                Name = "Semester 2021",
                Users = new List<User>
                {
                    developerUser
                }
            };
            developerUser.Projects.Add(projectTester);
            var updatedBug = new Bug
            {
                Id = 1,
                Name = "BugNuevo",
                Description = "Bug en el cliente",
                Version = "1.5",
                State = BugState.Done,
                ProjectId = 1
            };
            TestExceptionUtils.Throws<UserMustBeTesterException>(
                () => _bugRepository.Update(developerUser, updatedBug), "User's role must be tester for this action"
            );
        }

        [TestMethod]
        public void TesterUpdateBugWithoutNewProjectTest()
        {
            var testerUser = new User
            {
                Id = 2,
                FirstName = "Juan",
                LastName = "Rodriguez",
                Password = "pepe1234",
                UserName = "pp",
                Email = "pepe@gmail.com",
                Role = RoleType.Tester,
                Projects = new List<Project>()
            };
            using (var context = new BugSummaryContext(_contextOptions))
            {
                var projectTester = new Project
                {
                    Id = 1,
                    Name = "Semester 2021",
                    Users = new List<User>
                    {
                        testerUser
                    }
                };
                var projectTester2 = new Project
                {
                    Id = 2,
                    Name = "Semester 2021",
                    Users = new List<User>()
                };
                context.Projects.Add(projectTester);
                testerUser.Projects.Add(projectTester);
                //User does not have second project
                context.Projects.Add(projectTester2);
                var oldBug = new Bug
                {
                    Id = 1,
                    Name = "Bug1",
                    Description = "Bug en el servidor",
                    Version = "1.4",
                    State = BugState.Active,
                    ProjectId = 1
                };
                context.Add(oldBug);
                context.SaveChanges();
            }

            var updatedBug = new Bug
            {
                Id = 1,
                Name = "BugNuevo",
                Description = "Bug Nuevo",
                Version = "1.5",
                State = BugState.Done,
                ProjectId = 2
            };

            TestExceptionUtils.Throws<ProjectDontBelongToUser>(
                () => _bugRepository.Update(testerUser, updatedBug),
                "New project to update bug, does not belong to tester"
            );
        }

        [TestMethod]
        public void DeleteBug()
        {
            var testerUser = new User
            {
                Id = 2,
                FirstName = "Juan",
                LastName = "Rodriguez",
                Password = "pepe1234",
                UserName = "pp",
                Email = "pepe@gmail.com",
                Role = RoleType.Tester,
                Projects = new List<Project>()
            };
            using (var context = new BugSummaryContext(_contextOptions))
            {
                var projectTester = new Project
                {
                    Id = 1,
                    Name = "Semester 2021",
                    Users = new List<User>
                    {
                        testerUser
                    }
                };
                context.Projects.Add(projectTester);
                testerUser.Projects.Add(projectTester);
                var oldBug = new Bug
                {
                    Id = 1,
                    Name = "Bug1",
                    Description = "Bug en el servidor",
                    Version = "1.4",
                    State = BugState.Active,
                    ProjectId = 1,
                    Project = projectTester
                };
                context.Add(oldBug);
                context.SaveChanges();
            }

            var bugID = 1;


            _bugRepository.Delete(testerUser, bugID);
            _bugRepository.Save();

            using (var context = new BugSummaryContext(_contextOptions))
            {
                var databaseBug = context.Bugs.FirstOrDefault(p => p.Id == bugID);
                Assert.AreEqual(null, databaseBug);
            }
        }

        [TestMethod]
        public void DeveloperDeletesBug()
        {
            var developerUser = new User
            {
                Id = 2,
                Role = RoleType.Developer
            };
            var updatedBug = new Bug
            {
                Id = 1
            };
            TestExceptionUtils.Throws<UserMustBeTesterException>(
                () => _bugRepository.Delete(developerUser, updatedBug.Id), "User's role must be tester for this action"
            );
        }

        [TestMethod]
        public void DeletesInexistentBug()
        {
            var developerUser = new User
            {
                Id = 2,
                Role = RoleType.Tester
            };
            var updatedBug = new Bug
            {
                Id = 1
            };
            TestExceptionUtils.Throws<InexistentBugException>(
                () => _bugRepository.Delete(developerUser, updatedBug.Id), "The entered bug does not exist."
            );
        }
    }
}