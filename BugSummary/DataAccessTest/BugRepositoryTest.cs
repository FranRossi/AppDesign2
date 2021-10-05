﻿using System.Collections.Generic;
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
        public void AddNewBug()
        {
            Bug newBugToAdd = new Bug
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

            List<Bug> bugsExpected = new List<Bug>();
            bugsExpected.Add(newBugToAdd);

            using (var context = new BugSummaryContext(this._contextOptions))
            {
                List<Bug> bugsDataBase = context.Bugs.ToList();
                Assert.AreEqual(1, bugsDataBase.Count());
                CollectionAssert.AreEqual(bugsExpected, bugsDataBase, new BugComparer());
            }
        }
        
        [TestMethod]
        public void GetBug()
        {
            Bug newBugToAdd = new Bug
            {
                Id = 1,
                Name = "Bug1",
                Description = "Bug en el servidor",
                Version = "1.4",
                State = BugState.Active,
                Project = new Project{Id = 1},
                ProjectId = 1
            };
            using (var context = new BugSummaryContext(this._contextOptions))
            {
                context.Add(newBugToAdd);
                context.SaveChanges();
            }
            int bugId = newBugToAdd.Id;

            Bug bugDataBase =_bugRepository.Get(bugId);
            _bugRepository.Save();
            
            Assert.IsNotNull(bugDataBase);
            Assert.AreEqual(0, new BugComparer().Compare(newBugToAdd,bugDataBase));
        }

        [TestMethod]
        public void TesterAddsBugWithoutNewProjectTest()
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

            TestExceptionUtils.Throws<ProjectDoesntBelongToUserException>(
                () => _bugRepository.Add(testerUser, updatedBug), "The user is not assigned to the Project the bug belongs to."
            );
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
            _bugRepository.Update(testerUser, updatedBug);
            _bugRepository.Save();

            using (var context = new BugSummaryContext(this._contextOptions))
            {
                Bug databaseBug = context.Bugs.FirstOrDefault(u => u.Id == updatedBug.Id);
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
                                () => _bugRepository.Update(testerUser, updatedBug), "The entered bug does not exist."
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

            TestExceptionUtils.Throws<ProjectDoesntBelongToUserException>(
                () => _bugRepository.Update(testerUser, updatedBug), "The user is not assigned to the Project the bug belongs to."
            );
        }

        [TestMethod]
        public void FixAlreadyFixedBugTest()
        {
            User developerUser = new User
            {
                Id = 1,
                FirstName = "Juan",
                LastName = "Rodriguez",
                Password = "pepe1234",
                UserName = "pp",
                Email = "pepe@gmail.com",
                Role = RoleType.Developer,
                Projects = new List<Project>()
            };
            Bug bug = new Bug
            {
                Id = 1,
                Name = "Bug1",
                Description = "Bug en el servidor",
                Version = "1.4",
                State = BugState.Done,
                ProjectId = 1
            };
            Project projectTester = new Project()
            {
                Id = 1,
                Name = "Semester 2021",
            };
            using (var context = new BugSummaryContext(this._contextOptions))
            {
                context.Add(projectTester);
                context.Add(bug);
                context.SaveChanges();
            }

            TestExceptionUtils.Throws<BugAlreadyFixedException>(
                () => _bugRepository.FixBug(developerUser, bug.Id), "The bug you are trying to fix is already fixed."
            );
        }

        [TestMethod]
        public void DeleteBug()
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
                context.Projects.Add(projectTester);
                testerUser.Projects.Add(projectTester);
                Bug oldBug = new Bug
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
            int bugID = 1;


            _bugRepository.Delete(testerUser, bugID);
            _bugRepository.Save();

            using (var context = new BugSummaryContext(this._contextOptions))
            {
                Bug databaseBug = context.Bugs.FirstOrDefault(p => p.Id == bugID);
                Assert.AreEqual(null, databaseBug);
            }
        }

        [TestMethod]
        public void DeletesInexistentBug()
        {
            User developerUser = new User
            {
                Id = 2,
                Role = RoleType.Tester,
            };
            Bug updatedBug = new Bug
            {
                Id = 1,
            };
            TestExceptionUtils.Throws<InexistentBugException>(
                () => _bugRepository.Delete(developerUser, updatedBug.Id), "The entered bug does not exist."
            );
        }
    }
}