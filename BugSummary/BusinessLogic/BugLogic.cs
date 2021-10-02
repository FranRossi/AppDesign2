using BusinessLogicInterface;
using DataAccessInterface;
using Domain;
using System;


namespace BusinessLogic
{
    public class BugLogic
    {
        private IBugRepository _bugRepository;
        public BugLogic(IBugRepository bugRepository)
        {
            _bugRepository = bugRepository;
        }

        public void Add(User testerUser, Bug newBug)
        {
            _bugRepository.Add(testerUser, newBug);
            _bugRepository.Save();
        }
    }
}