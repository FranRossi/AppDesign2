using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using DataAccess;
using Domain;
using Domain.DomainUtilities;
using Domain.DomainUtilities.CustomExceptions;
using KellermanSoftware.CompareNetObjects;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestUtilities;
using Utilities.Comparers;
using Utilities.CustomExceptions;
namespace DataAccessTest
{

    [TestClass]
    public class ProjectRepositoryTest
    {
        private readonly DbConnection _connection;
        private readonly ProjectRepository _projectRepository;
        private readonly BugSummaryContext _bugSummaryContext;
        private readonly DbContextOptions<BugSummaryContext> _contextOptions;

        public ProjectRepositoryTest()
        {
            this._connection = new SqliteConnection("Filename=:memory:");
            this._contextOptions = new DbContextOptionsBuilder<BugSummaryContext>().UseSqlite(this._connection).Options;
            this._bugSummaryContext = new BugSummaryContext(this._contextOptions);
            this._projectRepository = new ProjectRepository(this._bugSummaryContext);
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
        public void AddNewProjectTest()
        {
            Project projectToAdd = new Project
            {
                Name = "New Project 2022"
            };

            _projectRepository.Add(projectToAdd);
            _projectRepository.Save();

            List<Project> projectsExpected = new List<Project>();
            projectsExpected.Add(new Project
            {
                Name = "New Project 2022",
                Id = 1,
                Bugs = new List<Bug>(),
                Users = new List<User>()
            });
            using (var context = new BugSummaryContext(this._contextOptions))
            {
                List<Project> projectsDataBase = context.Projects.ToList();
                Assert.AreEqual(1, projectsDataBase.Count());
                CollectionAssert.AreEqual(projectsExpected, projectsDataBase, new ProjectComparer());
            }
        }

        [TestMethod]
        public void AddAlreadyAddedProjectTest()
        {
            using (var context = new BugSummaryContext(this._contextOptions))
            {
                context.Add(new Project
                {
                    Name = "New Project 2022"
                });
                context.SaveChanges();
            }
            Project projectToAdd = new Project
            {
                Name = "New Project 2022"
            };


            TestExceptionUtils.Throws<ProjectNameIsNotUniqueException>(
                () => _projectRepository.Add(projectToAdd), "The project name chosen was already taken, please enter a different name."
            );
        }

        [TestMethod]
        public void GetAllProjectsFromRepositoryTest()
        {
            Bug bug1 = new Bug
            {
                Name = "Bug1",
                Description = "Bug en el servidor",
                Version = "1.4",
                State = BugState.Active,
            };
            Bug bug2 = new Bug
            {
                Name = "Bug2",
                Description = "Bug en el servidor 22",
                Version = "1.5",
                State = BugState.Fixed,
            };
            Bug bug3 = new Bug
            {
                Name = "Bug3",
                Description = "Bug en el servidor 232",
                Version = "3.5",
                State = BugState.Fixed,
            };
            using (var context = new BugSummaryContext(this._contextOptions))
            {
                context.Add(new Project
                {
                    Name = "New Project 2022",
                    Id = 1,
                    Bugs = new List<Bug> { bug1, bug2 },
                    Users = null
                });
                context.SaveChanges();
                context.Add(new Project
                {
                    Name = "New Project 2023",
                    Id = 2,
                    Bugs = new List<Bug> { bug3 },
                    Users = null
                });
                context.SaveChanges();

            }
            List<Project> projectsExpected = new List<Project>();
            projectsExpected.Add(new Project
            {
                Name = "New Project 2022",
                Id = 1,
                Bugs = new List<Bug> { bug1, bug2 },
                Users = null
            });
            projectsExpected.Add(new Project
            {
                Name = "New Project 2023",
                Id = 2,
                Bugs = new List<Bug> { bug3 },
                Users = null
            });

            List<Project> projectsDataBase = this._projectRepository.GetAll().ToList();

            Assert.AreEqual(2, projectsDataBase.Count());
            CompareLogic compareLogic = new CompareLogic();
            ComparisonResult deepComparisonResult = compareLogic.Compare(projectsExpected, projectsDataBase);
            Assert.IsTrue(deepComparisonResult.AreEqual);
        }

        [TestMethod]
        public void UpdateProjectTest()
        {
            Project newProject = new Project
            {
                Name = "Proyect 2344"
            };
            Project updatedProject = new Project
            {
                Id = 1,
                Name = "Proyect 2344"
            };
            using (var context = new BugSummaryContext(this._contextOptions))
            {
                context.Add(newProject);
                context.SaveChanges();
            }

            _projectRepository.Update(updatedProject);
            _projectRepository.Save();

            using (var context = new BugSummaryContext(this._contextOptions))
            {
                Project databaseProject = context.Projects.ToList().First(p => p.Id == newProject.Id);
                CompareLogic compareLogic = new CompareLogic();
                ComparisonResult deepComparisonResult = compareLogic.Compare(updatedProject, databaseProject);
                Assert.IsTrue(deepComparisonResult.AreEqual);
            }
        }

        [TestMethod]
        public void UpdateInexistentProjectTest()
        {
            Project updatedProject = new Project
            {
                Name = "Proyect 2344"
            };

            TestExceptionUtils.Throws<InexistentProjectException>(
               () => _projectRepository.Update(updatedProject), "The entered project does not exist."
            );
        }

        [TestMethod]
        public void DeleteProjectTest()
        {
            Project newProject = new Project
            {
                Name = "Proyect 2344"
            };
            int id = 1;
            using (var context = new BugSummaryContext(this._contextOptions))
            {
                context.Add(newProject);
                context.SaveChanges();
            }

            _projectRepository.Delete(id);
            _projectRepository.Save();

            using (var context = new BugSummaryContext(this._contextOptions))
            {
                Project databaseProject = context.Projects.FirstOrDefault(p => p.Id == id);
                Assert.AreEqual(null, databaseProject);
            }
        }

        [TestMethod]
        public void DeleteProjectWithBugsTest()
        {
            Bug newBug = new Bug
            {
                Id = 1,
                Name = "Bug1",
                Description = "Bug en el servidor",
                Version = "1.4",
                State = BugState.Active,
                ProjectId = 1
            };
            Project newProject = new Project
            {
                Name = "Proyect 2344",
                Bugs = new List<Bug> { newBug }
            };
            int id = 1;
            using (var context = new BugSummaryContext(this._contextOptions))
            {
                context.Add(newProject);
                context.SaveChanges();
            }

            _projectRepository.Delete(id);
            _projectRepository.Save();

            using (var context = new BugSummaryContext(this._contextOptions))
            {
                Project databaseProject = context.Projects.FirstOrDefault(p => p.Id == id);
                Bug databaseBug = context.Bugs.FirstOrDefault(b => b.Id == newBug.Id);
                Assert.AreEqual(null, databaseProject);
                Assert.AreEqual(null, databaseBug);
            }
        }

        [TestMethod]
        public void DeleteProjectWithUsersTest()
        {
            User newUser = new User
            {
                Id = 1,
                FirstName = "Pepe",
                LastName = "Perez",
                Password = "pepe1234",
                UserName = "pp",
                Email = "pepe@gmail.com",
                Role = RoleType.Admin,
            };
            Project newProject = new Project
            {
                Name = "Proyect 2344",
                Users = new List<User> { newUser }
            };
            int id = 1;
            using (var context = new BugSummaryContext(this._contextOptions))
            {
                context.Add(newProject);
                context.SaveChanges();
            }

            _projectRepository.Delete(id);
            _projectRepository.Save();

            using (var context = new BugSummaryContext(this._contextOptions))
            {
                Project databaseProject = context.Projects.FirstOrDefault(p => p.Id == id);
                User databaseUsers = context.Users.FirstOrDefault(b => b.Id == newUser.Id);
                User expectedUser = newUser;
                expectedUser.Token = null;
                expectedUser.Projects = null;
                Assert.AreEqual(null, databaseProject);
                CompareLogic compareLogic = new CompareLogic();
                ComparisonResult deepComparisonResult = compareLogic.Compare(expectedUser, databaseUsers);
                Assert.IsTrue(deepComparisonResult.AreEqual);
            }
        }


        [TestMethod]
        public void DeleteInexistentProjectTest()
        {
            int id = 1;

            TestExceptionUtils.Throws<InexistentProjectException>(
               () => _projectRepository.Delete(id), "The entered project does not exist."
            );
        }

        [TestMethod]
        public void AssignUserToProjectTest()
        {
            Project newProject = new Project
            {
                Id = 1,
                Name = "Proyect 2344"
            };
            User newUser = new User
            {
                Id = 1,
                FirstName = "Pepe",
                LastName = "Perez",
                Password = "pepe1234",
                UserName = "pp",
                Email = "pepe@gmail.com",
                Role = RoleType.Developer,
                Projects = null
            };
            int projectId = 1;
            int userId = 1;
            using (var context = new BugSummaryContext(this._contextOptions))
            {
                context.Add(newProject);
                context.Add(newUser);
                context.SaveChanges();
            }

            _projectRepository.AssignUserToProject(userId, projectId);
            _projectRepository.Save();

            newUser.Projects = new List<Project> { newProject };
            newProject.Users = new List<User> { newUser };
            using (var context = new BugSummaryContext(this._contextOptions))
            {
                Project databaseProject = context.Projects.Include("Users").FirstOrDefault(p => p.Id == projectId);
                CompareLogic compareLogic = new CompareLogic();
                ComparisonResult deepComparisonResult = compareLogic.Compare(newProject, databaseProject);
                Assert.IsTrue(deepComparisonResult.AreEqual);
            }
        }

        [TestMethod]
        public void AssignAlreadyAssignedUserToProjectTest()
        {
            Project newProject = new Project
            {
                Id = 1,
                Name = "Proyect 2344"
            };
            User newUser = new User
            {
                Id = 1,
                FirstName = "Pepe",
                LastName = "Perez",
                Password = "pepe1234",
                UserName = "pp",
                Email = "pepe@gmail.com",
                Role = RoleType.Developer,
                Projects = null
            };
            newProject.Users = new List<User> { newUser };
            int projectId = 1;
            int userId = 1;
            using (var context = new BugSummaryContext(this._contextOptions))
            {
                context.Add(newProject);
                context.SaveChanges();
            }

            _projectRepository.AssignUserToProject(userId, projectId);
            _projectRepository.Save();


            using (var context = new BugSummaryContext(this._contextOptions))
            {
                Project databaseProject = context.Projects.Include("Users").FirstOrDefault(p => p.Id == projectId);
                CompareLogic compareLogic = new CompareLogic();
                ComparisonResult deepComparisonResult = compareLogic.Compare(newProject, databaseProject);
                Assert.IsTrue(deepComparisonResult.AreEqual);
                Assert.AreEqual(1, databaseProject.Users.Count);
            }
        }

        [TestMethod]
        public void AssignInvalidUserToProjectTest()
        {
            Project newProject = new Project
            {
                Id = 1,
                Name = "Proyect 2344"
            };
            int projectId = 1;
            int userId = 1;
            using (var context = new BugSummaryContext(this._contextOptions))
            {
                context.Add(newProject);
                context.SaveChanges();
            }

            TestExceptionUtils.Throws<InexistentUserException>(
               () => _projectRepository.AssignUserToProject(userId, projectId), "The entered user does not exist."
            );
        }

        [TestMethod]
        public void AssignUserToInvalidProjectTest()
        {
            User newUser = new User
            {
                Id = 1,
                FirstName = "Pepe",
                LastName = "Perez",
                Password = "pepe1234",
                UserName = "pp",
                Email = "pepe@gmail.com",
                Role = RoleType.Developer,
                Projects = null
            };
            int projectId = 1;
            int userId = 1;
            using (var context = new BugSummaryContext(this._contextOptions))
            {
                context.Add(newUser);
                context.SaveChanges();
            }

            TestExceptionUtils.Throws<InexistentProjectException>(
               () => _projectRepository.AssignUserToProject(userId, projectId), "The entered project does not exist."
            );
        }

        [TestMethod]
        public void AssignInvalidRoleUserToProjectTest()
        {
            Project newProject = new Project
            {
                Id = 1,
                Name = "Proyect 2344"
            };
            User newUser = new User
            {
                Id = 1,
                FirstName = "Pepe",
                LastName = "Perez",
                Password = "pepe1234",
                UserName = "pp",
                Email = "pepe@gmail.com",
                Role = RoleType.Admin,
                Projects = null
            };
            int projectId = 1;
            int userId = 1;
            using (var context = new BugSummaryContext(this._contextOptions))
            {
                context.Add(newProject);
                context.Add(newUser);
                context.SaveChanges();
            }

            TestExceptionUtils.Throws<InvalidProjectAssigneeRoleException>(
               () => _projectRepository.AssignUserToProject(userId, projectId), "Project asingnees must either be Developers or Testers."
            );
        }

        [TestMethod]
        public void DissociateUserFromProject()
        {
            Project newProject = new Project
            {
                Id = 1,
                Name = "Proyect 2344"
            };
            User newUser = new User
            {
                Id = 1,
                FirstName = "Pepe",
                LastName = "Perez",
                Password = "pepe1234",
                UserName = "pp",
                Email = "pepe@gmail.com",
                Role = RoleType.Developer,
                Projects = null
            };
            newProject.Users = new List<User> { newUser };
            int projectId = 1;
            int userId = 1;
            using (var context = new BugSummaryContext(this._contextOptions))
            {
                context.Add(newProject);
                context.SaveChanges();
            }

            _projectRepository.DissociateUserFromProject(userId, projectId);
            _projectRepository.Save();

            newProject.Users = new List<User>();
            using (var context = new BugSummaryContext(this._contextOptions))
            {
                Project databaseProject = context.Projects.Include("Users").FirstOrDefault(p => p.Id == projectId);
                CompareLogic compareLogic = new CompareLogic();
                ComparisonResult deepComparisonResult = compareLogic.Compare(newProject, databaseProject);
                Assert.IsTrue(deepComparisonResult.AreEqual);
            }
        }

        [TestMethod]
        public void DissociateDissociatedUserFromProject()
        {
            Project newProject = new Project
            {
                Id = 1,
                Name = "Proyect 2344"
            };
            User newUser = new User
            {
                Id = 1,
                FirstName = "Pepe",
                LastName = "Perez",
                Password = "pepe1234",
                UserName = "pp",
                Email = "pepe@gmail.com",
                Role = RoleType.Developer,
                Projects = null
            };
            int projectId = 1;
            int userId = 1;
            using (var context = new BugSummaryContext(this._contextOptions))
            {
                context.Add(newProject);
                context.Add(newUser);
                context.SaveChanges();
            }

            _projectRepository.DissociateUserFromProject(userId, projectId);
            _projectRepository.Save();

            newProject.Users = new List<User>();
            using (var context = new BugSummaryContext(this._contextOptions))
            {
                Project databaseProject = context.Projects.Include("Users").FirstOrDefault(p => p.Id == projectId);
                CompareLogic compareLogic = new CompareLogic();
                ComparisonResult deepComparisonResult = compareLogic.Compare(newProject, databaseProject);
                Assert.IsTrue(deepComparisonResult.AreEqual);
            }
        }


        [TestMethod]
        public void DissociateInvalidUserFromProject()
        {
            Project newProject = new Project
            {
                Id = 1,
                Name = "Proyect 2344"
            };
            int projectId = 1;
            int userId = 1;
            using (var context = new BugSummaryContext(this._contextOptions))
            {
                context.Add(newProject);
                context.SaveChanges();
            }

            TestExceptionUtils.Throws<InexistentUserException>(
               () => _projectRepository.DissociateUserFromProject(userId, projectId), "The entered user does not exist."
            );
        }

        [TestMethod]
        public void DissociateUserFromInvalidProject()
        {
            User newUser = new User
            {
                Id = 1,
                FirstName = "Pepe",
                LastName = "Perez",
                Password = "pepe1234",
                UserName = "pp",
                Email = "pepe@gmail.com",
                Role = RoleType.Developer,
                Projects = null
            };
            int projectId = 1;
            int userId = 1;
            using (var context = new BugSummaryContext(this._contextOptions))
            {
                context.Add(newUser);
                context.SaveChanges();
            }

            TestExceptionUtils.Throws<InexistentProjectException>(
               () => _projectRepository.DissociateUserFromProject(userId, projectId), "The entered project does not exist."
            );
        }

        [TestMethod]
        public void AddBugsFromFile()
        {
            Bug newBug1 = new Bug
            {
                Name = "Bug1",
                Description = "Bug en el servidor",
                Version = "1.4",
                State = BugState.Active
            };
            Bug newBug2 = new Bug
            {
                Name = "Bug2",
                Description = "Bug en el mail",
                Version = "1.42",
                State = BugState.Active
            };
            Project newProject = new Project
            {
                Name = "Proyect 2344"
            };
            int id = 1;
            using (var context = new BugSummaryContext(this._contextOptions))
            {
                context.Add(newProject);
                context.SaveChanges();
            }
            newProject.Bugs = new List<Bug> { newBug1, newBug2 };

            _projectRepository.AddBugsFromFile(new List<Project> { newProject });
            _projectRepository.Save();

            newBug1.Id = 1;
            newBug2.Id = 2;
            List<Bug> expected = new List<Bug> { newBug1, newBug2 };
            using (var context = new BugSummaryContext(this._contextOptions))
            {
                Project databaseProject = context.Projects.Include("Bugs").FirstOrDefault(p => p.Id == id);
                List<Bug> result = databaseProject.Bugs;
                CompareLogic compareLogic = new CompareLogic();
                ComparisonResult deepComparisonResult = compareLogic.Compare(expected, result);
                Assert.IsTrue(deepComparisonResult.AreEqual);
            }
        }

        [TestMethod]
        public void AddOneBugFromFile()
        {
            Bug newBug2 = new Bug
            {
                Name = "Bug2",
                Description = "Bug en el mail",
                Version = "1.42",
                State = BugState.Active
            };
            Project newProject = new Project
            {
                Name = "Proyect 2344"
            };
            int id = 1;
            using (var context = new BugSummaryContext(this._contextOptions))
            {
                context.Add(newProject);
                context.SaveChanges();
            }
            newProject.Bugs = new List<Bug> { newBug2 };

            _projectRepository.AddBugsFromFile(new List<Project> { newProject });
            _projectRepository.Save();

            newBug2.Id = 1;
            List<Bug> expected = new List<Bug> { newBug2 };
            using (var context = new BugSummaryContext(this._contextOptions))
            {
                Project databaseProject = context.Projects.Include("Bugs").FirstOrDefault(p => p.Id == id);
                List<Bug> result = databaseProject.Bugs;
                CompareLogic compareLogic = new CompareLogic();
                ComparisonResult deepComparisonResult = compareLogic.Compare(expected, result);
                Assert.IsTrue(deepComparisonResult.AreEqual);
            }
        }

        [TestMethod]
        public void AddNoBugFromFile()
        {
            Project newProject = new Project
            {
                Name = "Proyect 2344"
            };
            int id = 1;
            using (var context = new BugSummaryContext(this._contextOptions))
            {
                context.Add(newProject);
                context.SaveChanges();
            }
            newProject.Bugs = new List<Bug> { };

            _projectRepository.AddBugsFromFile(new List<Project> { newProject });
            _projectRepository.Save();

            List<Bug> expected = new List<Bug> { };
            using (var context = new BugSummaryContext(this._contextOptions))
            {
                Project databaseProject = context.Projects.Include("Bugs").FirstOrDefault(p => p.Id == id);
                List<Bug> result = databaseProject.Bugs;
                CompareLogic compareLogic = new CompareLogic();
                ComparisonResult deepComparisonResult = compareLogic.Compare(expected, result);
                Assert.IsTrue(deepComparisonResult.AreEqual);
            }
        }

        [TestMethod]
        public void AddBugFromFileToInexistentProject()
        {
            Project newProject = new Project
            {
                Name = "Proyect 2344"
            };

            TestExceptionUtils.Throws<InexistentProjectException>(
               () => _projectRepository.AddBugsFromFile(new List<Project> { newProject }), "The entered project does not exist."
            );
        }
    }
}