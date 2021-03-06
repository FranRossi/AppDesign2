using BusinessLogicInterface;
using DataAccessInterface;
using Domain;
using System;
using System.Collections.Generic;

namespace BusinessLogic
{
    public class UserLogic : IUserLogic
    {
        private readonly IUserRepository _userRepository;

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
            return user;
        }

        public IEnumerable<Project> GetProjects(string token)
        {
            return _userRepository.GetProjects(token);
        }

        public IEnumerable<User> GetAll()
        {
            return _userRepository.GetAll();
        }
    }
}