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
        public BugLogic(IBugRepository bugRepository)
        {
            _bugRepository = bugRepository;
        }

        public void Add(User tester, Bug newBug)
        {
            _bugRepository.Add(tester, newBug);
            _bugRepository.Save();
        }

        public IEnumerable<Bug> GetAll(string token)
        {
            UserLogic userLogic = new UserLogic(_userRepository);
            User userByToken = userLogic.Get(token);
            return _bugRepository.GetAllByTester(userByToken);
        }
    }
}