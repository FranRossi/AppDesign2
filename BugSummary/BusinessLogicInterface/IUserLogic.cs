using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicInterface
{
    public interface IUserLogic
    {
        void Add(User newUser);

        User Get(string token);

        int GetFixedBugCount(int id);

        IEnumerable<Project> GetProjects(string token);

        IEnumerable<User> GetAll();
    }
}
