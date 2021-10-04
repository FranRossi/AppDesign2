using DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Common;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Domain;
using Domain.DomainUtilities;
using System.Collections.Generic;
using System.Linq;
using Utilities.Comparers;
using DataAccess.Exceptions;
using KellermanSoftware.CompareNetObjects;
using TestUtilities;
using Utilities.CustomExceptions;

namespace DataAccessTest
{
    [TestClass]
    public class BugRepositoryTest
    {

        private readonly DbConnection _connection;
        private readonly BugRepository _bugRepository;
        private readonly BugSummaryContext _bugSummaryContext;
        private readonly DbContextOptions<BugSummaryContext> _contextOptions;

        public BugRepositoryTest()
        {
            this._connection = new SqliteConnection("Filename=:memory:");
            this._contextOptions = new DbContextOptionsBuilder<BugSummaryContext>().UseSqlite(this._connection).Options;
            this._bugSummaryContext = new BugSummaryContext(this._contextOptions);
            this._bugRepository = new BugRepository(this._bugSummaryContext);
        }

        [TestInitialize]
        public void Setup()
        {
            this._connection.Open();
            this._bugSummaryContext.Database.EnsureCreated();
        }

        [TestCleanup]
        public void CleanUp()
        {
            this._bugSummaryContext.Database.EnsureDeleted();
        }

        [TestMethod]
        public void AddNewBugTest()
        {
            using (var context = new BugSummaryContext(this._contextOptions))
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
            }

            Bug newBug = new Bug
            {
                Id = 1,
                Name = "Bug1",
                Description = "Bug en el servidor",
                Version = "1.4",
                State = BugState.Active,
                Project = new Project(),
                ProjectId = 1
            };
            List<Bug> bugsExpected = new List<Bug>();
            bugsExpected.Add(newBug);


            using (var context = new BugSummaryContext(this._contextOptions))
            {
                List<Bug> bugsDataBase = context.Bugs.ToList();
                Assert.AreEqual(1, bugsDataBase.Count());
                CollectionAssert.AreEqual(bugsExpected, bugsDataBase, new BugComparer());
            }
        }

        [TestMethod]
        public void GetAllBugsFromRepositoryTest()
        {
            using (var context = new BugSummaryContext(this._contextOptions))
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

            Bug newBug1 = new Bug
            {
                Id = 1,
                Name = "Bug1",
                Description = "Bug en el servidor",
                Version = "1.4",
                State = BugState.Active,
                Project = new Project(),
                ProjectId = 1
            };
            Bug newBug2 = new Bug
            {
                Id = 2,
                Name = "Bug2",
                Description = "Bug en el cliente",
                Version = "1.4",
                State = BugState.Active,
                Project = new Project(),
                ProjectId = 1
            };
            List<Bug> bugsExpected = new List<Bug>();
            bugsExpected.Add(newBug1);
            bugsExpected.Add(newBug2);


            using (var context = new BugSummaryContext(this._contextOptions))
            {
                List<Bug> bugsDataBase = context.Bugs.ToList();
                Assert.AreEqual(2, bugsDataBase.Count());
                CollectionAssert.AreEqual(bugsExpected, bugsDataBase, new BugComparer());
            }

        }

        [TestMethod]
        public void GetAllBugsFromTester()
        {
            User testerUser = new User
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
            Project projectTester = new Project()
            {
                Id = 1,
                Name = "Semester 2021",
                Users = new List<User>
                {
                 testerUser
                }
            };
            testerUser.Projects.Add(projectTester);
            using (var context = new BugSummaryContext(this._contextOptions))
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

            List<Bug> bugsExpected = new List<Bug>()
            {
                new Bug
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

            List<Bug> bugsDataBase = this._bugRepository.GetAllByTester(testerUser).ToList();

            using (var context = new BugSummaryContext(this._contextOptions))
            {
                Assert.AreEqual(1, bugsDataBase.Count());
                CollectionAssert.AreEqual(bugsExpected, bugsDataBase, new BugComparer());
            }
        }

        [TestMethod]
        [ExpectedException(typeof(UserMustBeTesterException))]
        public void DeveloperCreatesBug()
        {
            User developerUser = new User
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
            Bug newBug = new Bug
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
            User testerUser = new User
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
            using (var context = new BugSummaryContext(this._contextOptions))
            {
                Project projectTester = new Project()
                {
                    Id = 1,
                    Name = "Semester 2021",
                    Users = new List<User>
                    {
                        testerUser
                    }
                };
                Project projectTester2 = new Project()
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
                Bug oldBug = new Bug
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
            
            Bug updatedBug = new Bug
            {
                Id = 1,
                Name = "BugNuevo",
                Description = "Bug Nuevo",
                Version = "1.5",
                State = BugState.Done,
                ProjectId = 2
            };
            _bugRepository.Update(testerUser,updatedBug);
            _bugRepository.Save();

            using (var context = new BugSummaryContext(this._contextOptions))
            {
                Bug databaseBug = context.Bugs.ToList().First(u => u.Id == updatedBug.Id);
                CompareLogic compareLogic = new CompareLogic();
                ComparisonResult deepComparisonResult = compareLogic.Compare(updatedBug, databaseBug);
                Assert.IsTrue(deepComparisonResult.AreEqual);
            }
        }
        
        [TestMethod]
        public void UpdateInexistentBugTest()
        {
            User testerUser = new User
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
            Project projectTester = new Project()
            {
                Id = 1,
                Name = "Semester 2021",
                Users = new List<User>
                {
                    testerUser
                }
            };
            testerUser.Projects.Add(projectTester);
            Bug updatedBug = new Bug
            {
                Id = 1,
                Name = "BugNuevo",
                Description = "Bug en el cliente",
                Version = "1.5",
                State = BugState.Done,
                ProjectId = 1
            };
            TestExceptionUtils.Throws<InexistentBugException>(
                () => _bugRepository.Update(testerUser,updatedBug), "The bug to update does not exist on database, please enter a different bug"
            );
        }
        
        [TestMethod]
        public void DeveloperUpdateBugTest()
        {
            User developerUser = new User
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
            Project projectTester = new Project()
            {
                Id = 1,
                Name = "Semester 2021",
                Users = new List<User>
                {
                    developerUser
                }
            };
            developerUser.Projects.Add(projectTester);
            Bug updatedBug = new Bug
            {
                Id = 1,
                Name = "BugNuevo",
                Description = "Bug en el cliente",
                Version = "1.5",
                State = BugState.Done,
                ProjectId = 1
            };
            TestExceptionUtils.Throws<UserMustBeTesterException>(
                () => _bugRepository.Update(developerUser,updatedBug), "User's role must be tester for this action"
            );
        }
        
          [TestMethod]
        public void TesterUpdateBugWithoutNewProjectTest()
        {
            User testerUser = new User
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
            using (var context = new BugSummaryContext(this._contextOptions))
            {
                Project projectTester = new Project()
                {
                    Id = 1,
                    Name = "Semester 2021",
                    Users = new List<User>
                    {
                        testerUser
                    }
                };
                Project projectTester2 = new Project()
                {
                    Id = 2,
                    Name = "Semester 2021",
                    Users = new List<User>()
                };
                context.Projects.Add(projectTester);
                testerUser.Projects.Add(projectTester);
                //User does not have second project
                context.Projects.Add(projectTester2);
                Bug oldBug = new Bug
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
            
            Bug updatedBug = new Bug
            {
                Id = 1,
                Name = "BugNuevo",
                Description = "Bug Nuevo",
                Version = "1.5",
                State = BugState.Done,
                ProjectId = 2
            };
            
            TestExceptionUtils.Throws<ProjectDontBelongToUser>(
                () => _bugRepository.Update(testerUser,updatedBug), "New project to update bug, does not belong to tester"
            );
        }
        
    }
}
