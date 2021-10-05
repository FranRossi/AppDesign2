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
        private UserLogic _userLogic;
        public BugLogic(IBugRepository bugRepository, IUserRepository userRepository)
        {
            _bugRepository = bugRepository;
            _userRepository = userRepository;
            _userLogic = new UserLogic(_userRepository);
        }

        public void Add(User tester, Bug newBug)
        {
            _bugRepository.Add(tester, newBug);
            _bugRepository.Save();
        }

        public IEnumerable<Bug> GetAll(string token)
        {
            User userByToken = _userLogic.Get(token);
            return _bugRepository.GetAllByTester(userByToken);
        }

        public void Update(string token, Bug updatedBug)
        {
            User userByToken = _userLogic.Get(token);
            _bugRepository.Update(userByToken, updatedBug);
            _bugRepository.Save();
        }

        public void FixBug(string token, int bugId)
        {
            User userByToken = _userLogic.Get(token);
            _bugRepository.FixBug(userByToken, bugId);
            _bugRepository.Save();
        }
    }
}