using System.Collections.Generic;
using Domain;
using Domain.DomainUtilities;

namespace DataAccessInterface
{
    public interface IUserRepository
    {
        void Add(User user);

        bool Authenticate(string username, string password);

        void UpdateToken(string username, string token);

        RoleType GetRoleByToken(string token);

        User Get(string token);

        User Get(int id);

        void Save();

        IEnumerable<Project> GetProjects(string token);

        IEnumerable<User> GetAll();
    }
}