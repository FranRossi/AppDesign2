using System;
using System.Linq;
using BusinessLogicInterface;
using DataAccessInterface;
using Domain.DomainUtilities;
using Utilities.CustomExceptions;

namespace BusinessLogic
{
    public class SessionLogic : ISessionLogic
    {
        private readonly IUserRepository _userRepository;

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
                _userRepository.Save();
            }
            else
            {
                throw new LoginException();
            }
            return token;
        }

        public RoleType GetRoleByToken(string token)
        {
            return _userRepository.GetRoleByToken(token);
        }
    }
}