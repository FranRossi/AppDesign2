using BusinessLogicInterface;
using DataAccessInterface;
using Domain;
using System;
using System.Collections.Generic;


namespace BusinessLogic
{
    public class BugLogic : IBugLogic
    {
        private IBugRepository _bugRepository;
        private IUserRepository _userRepository;
        public BugLogic(IBugRepository bugRepository, IUserRepository userRepository)
        {
            _bugRepository = bugRepository;
            _userRepository = userRepository;
        }

        public void Add(string token, Bug newBug)
        {
            UserLogic userLogic = new UserLogic(_userRepository);
            User userByToken = userLogic.Get(token);
            _bugRepository.Add(userByToken, newBug);
            _bugRepository.Save();
        }

        public IEnumerable<Bug> GetAll(string token)
        {
            UserLogic userLogic = new UserLogic(_userRepository);
            User userByToken = userLogic.Get(token);
            return _bugRepository.GetAllByTester(userByToken);
        }

        public void Update(string token, Bug updatedBug)
        {
            UserLogic userLogic = new UserLogic(_userRepository);
            User userByToken = userLogic.Get(token);
            _bugRepository.Update(userByToken,updatedBug);
            _bugRepository.Save();
        }

        public void Delete(string token, int bugId)
        {
            UserLogic userLogic = new UserLogic(_userRepository);
            User userByToken = userLogic.Get(token);
            _bugRepository.Delete(userByToken,bugId);
            _bugRepository.Save();
        }
    }
}