using DataAccessInterface;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class SessionLogic
    {
        private IRepository<User> _userRepository;
        public SessionLogic(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public string GenerateToken()
        {
            return "hprpxbGF2UjU5uANp+hmQqgAQ1eSiLbv";
        }
    }
}
