﻿using DataAccessInterface;
using Domain;
using System;

namespace BusinessLogic
{
    public class UserLogic
    {
        private IRepository<User> _userRepository;
        public UserLogic(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public void Add(User newUser)
        {
            _userRepository.Add(newUser);
            _userRepository.Save();
        }
    }
}