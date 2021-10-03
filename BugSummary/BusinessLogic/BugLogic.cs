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
        public BugLogic(IBugRepository bugRepository)
        {
            _bugRepository = bugRepository;
        }


        public void Add(User tester, Bug newBug)
        {
            _bugRepository.Add(tester, newBug);
            _bugRepository.Save();
        }

        public IEnumerable<Bug> GetAll(User user)
        {
            return new List<Bug>();
        }
    }
}