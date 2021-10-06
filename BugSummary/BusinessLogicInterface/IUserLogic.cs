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
        public void Add(User newUser);
        public User Get(string token);
        public int GetFixedBugCount(int id);
    }
}
