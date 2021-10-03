using BusinessLogicInterface;
using DataAccessInterface;
using Domain;
using System;

namespace BusinessLogic
{
    public class UserLogic : ILogic<User>
    {
        private IUserRepository _userRepository;
        public UserLogic(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void Add(User newUser)
        {
            _userRepository.Add(newUser);
            _userRepository.Save();
        }

        public User Get(string token)
        {
            User user = _userRepository.Get(token);
            _userRepository.Save();
            return user;
        }
    }
}
