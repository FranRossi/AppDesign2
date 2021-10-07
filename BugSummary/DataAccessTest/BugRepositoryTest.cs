using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using CustomExceptions;
using DataAccess;
using Domain;
using Domain.DomainUtilities;
using KellermanSoftware.CompareNetObjects;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestUtilities;
using Utilities.Comparers;
using Utilities.Criterias;

namespace DataAccessTest
{
    [TestClass]
    [ExcludeFromCodeCoverage]
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
        public void GetBug()
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
            Bug bug = new Bug
            {
                Id = 1,
                Name = "Bug1",
                Description = "Bug en el servidor",
                Version = "1.4",
                State = BugState.Active,
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
                bug.ProjectId = 1;

                context.Add(bug);
                context.SaveChanges();
            }

            int bugId = 1;
            Bug bugDataBase = _bugRepository.Get(testerUser, bugId);

            Assert.IsNotNull(bugDataBase);
            Assert.AreEqual(0, new BugComparer().Compare(bug, bugDataBase));
        }
        
        [TestMethod]
        public void GetBugByAdmin()
        {
            User adminUser = new User
            {
                Id = 2,
                FirstName = "Juan",
                LastName = "Rodriguez",
                Password = "pepe1234",
                UserName = "pp",
                Email = "pepe@gmail.com",
                Role = RoleType.Admin
            };
            Bug bug = new Bug
            {
                Id = 1,
                Name = "Bug1",
                Description = "Bug en el servidor",
                Version = "1.4",
                State = BugState.Active,
            };
            using (var context = new BugSummaryContext(this._contextOptions))
            {
                context.Add(bug);
                context.SaveChanges();
            }

            int bugId = 1;
            Bug bugDataBase = _bugRepository.Get(adminUser, bugId);

            Assert.IsNotNull(bugDataBase);
            Assert.AreEqual(0, new BugComparer().Compare(bug, bugDataBase));
        }

        [TestMethod]
        public void GetInvalidBug()
        {
            User testerUser = new User { Id = 1 };
            Bug newBugToAdd = new Bug
            {
                Id = 1,
                Name = "Bug1",
                Description = "Bug en el servidor",
                Version = "1.4",
                State = BugState.Active,
                Project = new Project { Id = 1 },
                ProjectId = 1
            };

            int bugId = newBugToAdd.Id;

            TestExceptionUtils.Throws<InexistentBugException>(
                () => _bugRepository.Get(testerUser, bugId), "The entered bug does not exist."
            );
        }

        [TestMethod]
        public void TesterGetsBugWithoutNewProject()
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
                    Users = new List<User> { }
                };
                context.Projects.Add(projectTester);
                Bug bug = new Bug
                {
                    Id = 1,
                    Name = "Bug1",
                    Description = "Bug en el servidor",
                    Version = "1.4",
                    State = BugState.Active,
                    ProjectId = 1
                };
                context.Add(bug);
                context.SaveChanges();
            }

            int bugId = 1;
            TestExceptionUtils.Throws<UserIsNotAssignedToProjectException>(
                () => _bugRepository.Get(testerUser, bugId), "The user is not assigned to the Project the bug belongs to."
            );
        }

        [TestMethod]
        public void AddNewBugTest()
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
                context.SaveChanges();
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
            List<Bug> bugsExpected = new List<Bug>();
            bugsExpected.Add(newBug);

            _bugRepository.Add(testerUser, newBug);
            _bugRepository.Save();

            using (var context = new BugSummaryContext(this._contextOptions))
            {
                List<Bug> bugsDataBase = context.Bugs.ToList();
                Assert.AreEqual(1, bugsDataBase.Count());
                CollectionAssert.AreEqual(bugsExpected, bugsDataBase, new BugComparer());
            }
        }

        [TestMethod]
        public void TesterAddsBugWithoutNewProject()
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
                State = BugState.Fixed,
                ProjectId = 2
            };

            TestExceptionUtils.Throws<UserIsNotAssignedToProjectException>(
                () => _bugRepository.Add(testerUser, updatedBug), "The user is not assigned to the Project the bug belongs to."
            );
        }

        [TestMethod]
        public void GetAllBugsFiltered()
        {
            Bug oldBug = new Bug
            {
                Id = 1,
                Name = "Bug1",
                Description = "Bug en el servidor",
                Version = "1.4",
                State = BugState.Active,
            };
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
                    Users = new List<User> { }
                };
                context.Projects.Add(projectTester);
                testerUser.Projects.Add(projectTester);
                oldBug.ProjectId = 1;
                context.Add(oldBug);
                context.SaveChanges();
            }

            List<Bug> bugsExpected = new List<Bug>();
            bugsExpected.Add(oldBug);
            BugSearchCriteria criteria = new BugSearchCriteria()
            {
                Name = "Bug1",
                State = BugState.Active,
                ProjectId = 1,
                Id = 1
            };
            IEnumerable<Bug> bugsDataBase = _bugRepository.GetAllFiltered(testerUser, criteria.MatchesCriteria);

            using (var context = new BugSummaryContext(this._contextOptions))
            {
                Assert.AreEqual(1, bugsDataBase.Count());
                CollectionAssert.AreEqual(bugsExpected, (ICollection)bugsDataBase, new BugComparer());
            }
        }


        [TestMethod]
        public void BugMatchesCriteria()
        {
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
            BugSearchCriteria criteria = new BugSearchCriteria()
            {
                Name = "Bug1",
                State = BugState.Active,
                ProjectId = 1,
                Id = 1
            };
            bool matches = criteria.MatchesCriteria(newBug1);
            Assert.IsTrue(matches);
        }

        [TestMethod]
        public void CreateBugCriteria()
        {
            BugSearchCriteria criteria = new BugSearchCriteria()
            {
                Name = "Bug1",
                State = BugState.Active,
                ProjectId = 1,
                Id = 1
            };
            BugSearchCriteria criteria2 = new BugSearchCriteria()
            {
                Name = "Bug1",
                State = BugState.Active,
                ProjectId = 1,
                Id = 1
            };

            CompareLogic compareLogic = new CompareLogic();
            ComparisonResult deepComparisonResult = compareLogic.Compare(criteria, criteria2);
            Assert.IsTrue(deepComparisonResult.AreEqual);
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
                State = BugState.Fixed,
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
                State = BugState.Fixed,
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
                State = BugState.Fixed,
                ProjectId = 2
            };

            TestExceptionUtils.Throws<UserIsNotAssignedToProjectException>(
                () => _bugRepository.Update(testerUser, updatedBug), "The user is not assigned to the Project the bug belongs to."
            );
        }
        [TestMethod]
        public void FixBugTest()
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
                State = BugState.Active,
                ProjectId = 1
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
            Project projectTester2 = new Project()
            {
                Id = 2,
                Name = "Semester 2021",
                Users = new List<User>
                    {
                        developerUser
                    }
            };
            using (var context = new BugSummaryContext(this._contextOptions))
            {
                context.Projects.Add(projectTester);
                developerUser.Projects.Add(projectTester);
                context.Projects.Add(projectTester2);
                developerUser.Projects.Add(projectTester2);
                context.Add(bug);
                context.SaveChanges();
            }


            _bugRepository.Fix(developerUser, bug.Id);
            _bugRepository.Save();

            using (var context = new BugSummaryContext(this._contextOptions))
            {
                Bug databaseBug = context.Bugs.Include("Fixer").Include("Project").First();
                User fixer = databaseBug.Fixer;
                developerUser.Projects = null;
                fixer.FixedBugs = null;
                CompareLogic compareLogic = new CompareLogic();
                ComparisonResult deepComparisonResult = compareLogic.Compare(developerUser, fixer);
                Assert.IsTrue(deepComparisonResult.AreEqual);
                Assert.AreEqual(BugState.Fixed, databaseBug.State);
            }
        }

        [TestMethod]
        public void FixInvalidBugTest()
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
                State = BugState.Active,
                ProjectId = 1
            };

            TestExceptionUtils.Throws<InexistentBugException>(
                () => _bugRepository.Fix(developerUser, bug.Id), "The entered bug does not exist."
            );
        }

        [TestMethod]
        public void FixBugFromOtherProjectTest()
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
                State = BugState.Active,
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

            TestExceptionUtils.Throws<UserIsNotAssignedToProjectException>(
                () => _bugRepository.Fix(developerUser, bug.Id), "The user is not assigned to the Project the bug belongs to."
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
                State = BugState.Fixed,
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
                () => _bugRepository.Fix(developerUser, bug.Id), "The bug you are trying to fix is already fixed."
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

        [TestMethod]
        public void TesterDeleteBugWithoutNewProjectTest()
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
            Bug bug = new Bug
            {
                Id = 1,
                Name = "Bug1",
                Description = "Bug en el servidor",
                Version = "1.4",
                State = BugState.Active,
            };
            using (var context = new BugSummaryContext(this._contextOptions))
            {
                Project projectTester = new Project()
                {
                    Id = 1,
                    Name = "Semester 2021",
                    Users = new List<User> { }
                };
                context.Projects.Add(projectTester);
                bug.ProjectId = projectTester.Id;
                context.Add(bug);
                context.SaveChanges();
            }
            TestExceptionUtils.Throws<UserIsNotAssignedToProjectException>(
                () => _bugRepository.Delete(testerUser, bug.Id), "The user is not assigned to the Project the bug belongs to."
            );
        }
    }
}