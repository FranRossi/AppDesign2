using BusinessLogicInterface;
using DataAccessInterface;
using Domain;
using Domain.DomainUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.CustomExceptions;

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
                _userRepository.Save();
            }
            else
                throw new LoginException();
            return token;
        }

        public RoleType GetRoleByToken(string token)
        {
            return _userRepository.GetRoleByToken(token);
        }
    }
}
