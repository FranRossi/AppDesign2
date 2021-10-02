﻿using BusinessLogicInterface;
using DataAccessInterface;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class SessionLogic : ISessionLogic
    {
        private IUserRepository _userRepository;
        public SessionLogic(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public string GenerateToken()
        {
            byte[] time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
            byte[] key = Guid.NewGuid().ToByteArray();
            string token = Convert.ToBase64String(time.Concat(key).ToArray());
            return token;
        }

        public string Authenticate(string username, string pass)
        {
            string token = null;
            if (_userRepository.Authenticate(username, pass))
            {
                token = GenerateToken();
                _userRepository.UpdateToken(username, token);
            }
            return token;
        }
    }
}