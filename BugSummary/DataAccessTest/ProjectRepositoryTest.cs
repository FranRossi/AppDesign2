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
        private readonly BugSummaryContext _bugSummaryContext;
        private readonly DbConnection _connection;
        private readonly DbContextOptions<BugSummaryContext> _contextOptions;
        private readonly ProjectRepository _projectRepository;

        public ProjectRepositoryTest()
        {
            _connection = new SqliteConnection("Filename=:memory:");
            _contextOptions = new DbContextOptionsBuilder<BugSummaryContext>().UseSqlite(_connection).Options;
            _bugSummaryContext = new BugSummaryContext(_contextOptions);
            _projectRepository = new ProjectRepository(_bugSummaryContext);
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
        public void AddNewProjectTest()
        {
            var projectToAdd = new Project
            {
                Name = "New Project 2022"
            };

            _projectRepository.Add(projectToAdd);
            _projectRepository.Save();

            var projectsExpected = new List<Project>();
            projectsExpected.Add(new Project
            {
                Name = "New Project 2022",
                Id = 1,
                Bugs = new List<Bug>(),
                Users = new List<User>()
            });
            using (var context = new BugSummaryContext(_contextOptions))
            {
                var projectsDataBase = context.Projects.ToList();
                Assert.AreEqual(1, projectsDataBase.Count());
                CollectionAssert.AreEqual(projectsExpected, projectsDataBase, new ProjectComparer());
            }
        }

        [TestMethod]
        public void AddAlreadyAddedProjectTest()
        {
            using (var context = new BugSummaryContext(_contextOptions))
            {
                context.Add(new Project
                {
                    Name = "New Project 2022"
                });
                context.SaveChanges();
            }

            var projectToAdd = new Project
            {
                Name = "New Project 2022"
            };


            TestExceptionUtils.Throws<ProjectNameIsNotUniqueException>(
                () => _projectRepository.Add(projectToAdd),
                "The project name chosen was already taken, please enter a different name"
            );
        }

        [TestMethod]
        public void GetAllProjectsFromRepositoryTest()
        {
            using (var context = new BugSummaryContext(_contextOptions))
            {
                context.Add(new Project
                {
                    Name = "New Project 2022",
                    Id = 1,
                    Bugs = new List<Bug>(),
                    Users = new List<User>()
                });
                context.SaveChanges();
                context.Add(new Project
                {
                    Name = "New Project 2023",
                    Id = 2,
                    Bugs = new List<Bug>(),
                    Users = new List<User>()
                });
                context.SaveChanges();
            }

            var projectsExpected = new List<Project>();
            projectsExpected.Add(new Project
            {
                Name = "New Project 2022",
                Id = 1,
                Bugs = new List<Bug>(),
                Users = new List<User>()
            });
            projectsExpected.Add(new Project
            {
                Name = "New Project 2023",
                Id = 2,
                Bugs = new List<Bug>(),
                Users = new List<User>()
            });

            var projectsDataBase = _projectRepository.GetAll().ToList();

            Assert.AreEqual(2, projectsDataBase.Count());
            CollectionAssert.AreEqual(projectsExpected, projectsDataBase, new ProjectComparer());
        }

        [TestMethod]
        public void UpdateProjectTest()
        {
            var newProject = new Project
            {
                Name = "Proyect 2344"
            };
            var updatedProject = new Project
            {
                Id = 1,
                Name = "Proyect 2344"
            };
            using (var context = new BugSummaryContext(_contextOptions))
            {
                context.Add(newProject);
                context.SaveChanges();
            }

            _projectRepository.Update(updatedProject);
            _projectRepository.Save();

            using (var context = new BugSummaryContext(_contextOptions))
            {
                var databaseProject = context.Projects.ToList().First(p => p.Id == newProject.Id);
                var compareLogic = new CompareLogic();
                var deepComparisonResult = compareLogic.Compare(updatedProject, databaseProject);
                Assert.IsTrue(deepComparisonResult.AreEqual);
            }
        }

        [TestMethod]
        public void UpdateInexistentProjectTest()
        {
            var updatedProject = new Project
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
            var newProject = new Project
            {
                Name = "Proyect 2344"
            };
            var id = 1;
            using (var context = new BugSummaryContext(_contextOptions))
            {
                context.Add(newProject);
                context.SaveChanges();
            }

            _projectRepository.Delete(id);
            _projectRepository.Save();

            using (var context = new BugSummaryContext(_contextOptions))
            {
                var databaseProject = context.Projects.FirstOrDefault(p => p.Id == id);
                Assert.AreEqual(null, databaseProject);
            }
        }

        [TestMethod]
        public void DeleteProjectWithBugsTest()
        {
            var newBug = new Bug
            {
                Id = 1,
                Name = "Bug1",
                Description = "Bug en el servidor",
                Version = "1.4",
                State = BugState.Active,
                ProjectId = 1
            };
            var newProject = new Project
            {
                Name = "Proyect 2344",
                Bugs = new List<Bug> {newBug}
            };
            var id = 1;
            using (var context = new BugSummaryContext(_contextOptions))
            {
                context.Add(newProject);
                context.SaveChanges();
            }

            _projectRepository.Delete(id);
            _projectRepository.Save();

            using (var context = new BugSummaryContext(_contextOptions))
            {
                var databaseProject = context.Projects.FirstOrDefault(p => p.Id == id);
                var databaseBug = context.Bugs.FirstOrDefault(b => b.Id == newBug.Id);
                Assert.AreEqual(null, databaseProject);
                Assert.AreEqual(null, databaseBug);
            }
        }

        [TestMethod]
        public void DeleteProjectWithUsersTest()
        {
            var newUser = new User
            {
                Id = 1,
                FirstName = "Pepe",
                LastName = "Perez",
                Password = "pepe1234",
                UserName = "pp",
                Email = "pepe@gmail.com",
                Role = RoleType.Admin
            };
            var newProject = new Project
            {
                Name = "Proyect 2344",
                Users = new List<User> {newUser}
            };
            var id = 1;
            using (var context = new BugSummaryContext(_contextOptions))
            {
                context.Add(newProject);
                context.SaveChanges();
            }

            _projectRepository.Delete(id);
            _projectRepository.Save();

            using (var context = new BugSummaryContext(_contextOptions))
            {
                var databaseProject = context.Projects.FirstOrDefault(p => p.Id == id);
                var databaseUsers = context.Users.FirstOrDefault(b => b.Id == newUser.Id);
                var expectedUser = newUser;
                expectedUser.Token = null;
                expectedUser.Projects = null;
                Assert.AreEqual(null, databaseProject);
                var compareLogic = new CompareLogic();
                var deepComparisonResult = compareLogic.Compare(expectedUser, databaseUsers);
                Assert.IsTrue(deepComparisonResult.AreEqual);
            }
        }


        [TestMethod]
        public void DeleteInexistentProjectTest()
        {
            var id = 1;

            TestExceptionUtils.Throws<InexistentProjectException>(
                () => _projectRepository.Delete(id), "The entered project does not exist."
            );
        }

        [TestMethod]
        public void AssignUserToProjectTest()
        {
            var newProject = new Project
            {
                Id = 1,
                Name = "Proyect 2344"
            };
            var newUser = new User
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
            var projectId = 1;
            var userId = 1;
            using (var context = new BugSummaryContext(_contextOptions))
            {
                context.Add(newProject);
                context.Add(newUser);
                context.SaveChanges();
            }

            _projectRepository.AssignUserToProject(userId, projectId);
            _projectRepository.Save();

            newUser.Projects = new List<Project> {newProject};
            newProject.Users = new List<User> {newUser};
            using (var context = new BugSummaryContext(_contextOptions))
            {
                var databaseProject = context.Projects.Include("Users").FirstOrDefault(p => p.Id == projectId);
                var compareLogic = new CompareLogic();
                var deepComparisonResult = compareLogic.Compare(newProject, databaseProject);
                Assert.IsTrue(deepComparisonResult.AreEqual);
            }
        }

        [TestMethod]
        public void AssignAlreadyAssignedUserToProjectTest()
        {
            var newProject = new Project
            {
                Id = 1,
                Name = "Proyect 2344"
            };
            var newUser = new User
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
            newProject.Users = new List<User> {newUser};
            var projectId = 1;
            var userId = 1;
            using (var context = new BugSummaryContext(_contextOptions))
            {
                context.Add(newProject);
                context.SaveChanges();
            }

            _projectRepository.AssignUserToProject(userId, projectId);
            _projectRepository.Save();


            using (var context = new BugSummaryContext(_contextOptions))
            {
                var databaseProject = context.Projects.Include("Users").FirstOrDefault(p => p.Id == projectId);
                var compareLogic = new CompareLogic();
                var deepComparisonResult = compareLogic.Compare(newProject, databaseProject);
                Assert.IsTrue(deepComparisonResult.AreEqual);
                Assert.AreEqual(1, databaseProject.Users.Count);
            }
        }

        [TestMethod]
        public void AssignInvalidUserToProjectTest()
        {
            var newProject = new Project
            {
                Id = 1,
                Name = "Proyect 2344"
            };
            var projectId = 1;
            var userId = 1;
            using (var context = new BugSummaryContext(_contextOptions))
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
            var newUser = new User
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
            var projectId = 1;
            var userId = 1;
            using (var context = new BugSummaryContext(_contextOptions))
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
            var newProject = new Project
            {
                Id = 1,
                Name = "Proyect 2344"
            };
            var newUser = new User
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
            var projectId = 1;
            var userId = 1;
            using (var context = new BugSummaryContext(_contextOptions))
            {
                context.Add(newProject);
                context.Add(newUser);
                context.SaveChanges();
            }

            TestExceptionUtils.Throws<InvalidProjectAssigneeRoleException>(
                () => _projectRepository.AssignUserToProject(userId, projectId),
                "Project asingnees must either be Developers or Testers."
            );
        }

        [TestMethod]
        public void DissociateUserFromProject()
        {
            var newProject = new Project
            {
                Id = 1,
                Name = "Proyect 2344"
            };
            var newUser = new User
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
            newProject.Users = new List<User> {newUser};
            var projectId = 1;
            var userId = 1;
            using (var context = new BugSummaryContext(_contextOptions))
            {
                context.Add(newProject);
                context.SaveChanges();
            }

            _projectRepository.DissociateUserFromProject(userId, projectId);
            _projectRepository.Save();

            newProject.Users = new List<User>();
            using (var context = new BugSummaryContext(_contextOptions))
            {
                var databaseProject = context.Projects.Include("Users").FirstOrDefault(p => p.Id == projectId);
                var compareLogic = new CompareLogic();
                var deepComparisonResult = compareLogic.Compare(newProject, databaseProject);
                Assert.IsTrue(deepComparisonResult.AreEqual);
            }
        }

        [TestMethod]
        public void DissociateDissociatedUserFromProject()
        {
            var newProject = new Project
            {
                Id = 1,
                Name = "Proyect 2344"
            };
            var newUser = new User
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
            var projectId = 1;
            var userId = 1;
            using (var context = new BugSummaryContext(_contextOptions))
            {
                context.Add(newProject);
                context.Add(newUser);
                context.SaveChanges();
            }

            _projectRepository.DissociateUserFromProject(userId, projectId);
            _projectRepository.Save();

            newProject.Users = new List<User>();
            using (var context = new BugSummaryContext(_contextOptions))
            {
                var databaseProject = context.Projects.Include("Users").FirstOrDefault(p => p.Id == projectId);
                var compareLogic = new CompareLogic();
                var deepComparisonResult = compareLogic.Compare(newProject, databaseProject);
                Assert.IsTrue(deepComparisonResult.AreEqual);
            }
        }


        [TestMethod]
        public void DissociateInvalidUserFromProject()
        {
            var newProject = new Project
            {
                Id = 1,
                Name = "Proyect 2344"
            };
            var projectId = 1;
            var userId = 1;
            using (var context = new BugSummaryContext(_contextOptions))
            {
                context.Add(newProject);
                context.SaveChanges();
            }

            TestExceptionUtils.Throws<InexistentUserException>(
                () => _projectRepository.DissociateUserFromProject(userId, projectId),
                "The entered user does not exist."
            );
        }

        [TestMethod]
        public void DissociateUserFromInvalidProject()
        {
            var newUser = new User
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
            var projectId = 1;
            var userId = 1;
            using (var context = new BugSummaryContext(_contextOptions))
            {
                context.Add(newUser);
                context.SaveChanges();
            }

            TestExceptionUtils.Throws<InexistentProjectException>(
                () => _projectRepository.DissociateUserFromProject(userId, projectId),
                "The entered project does not exist."
            );
        }
    }
}