using System;
using System.Collections.Generic;
using BusinessLogicInterface;
using DataAccessInterface;
using Domain;
using Utilities.Criterias;


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

        public void Add(string token, Bug newBug)
        {
            UserLogic userLogic = new UserLogic(_userRepository);
            User userByToken = userLogic.Get(token);
            _bugRepository.Add(userByToken, newBug);
            _bugRepository.Save();
        }

        public IEnumerable<Bug> GetAll(string token)
        {
            User userByToken = _userLogic.Get(token);
            return _bugRepository.GetAllByUser(userByToken);
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

        public void Delete(string token, int bugId)
        {
            UserLogic userLogic = new UserLogic(_userRepository);
            User userByToken = userLogic.Get(token);
            _bugRepository.Delete(userByToken,bugId);
            _bugRepository.Save();
        }

        public Bug Get(string token, int bugId)
        {
            UserLogic userLogic = new UserLogic(_userRepository);
            User userByToken = userLogic.Get(token);
            Bug dataBaseBug =_bugRepository.Get(userByToken,bugId);
            return dataBaseBug;
        }

        public IEnumerable<Bug> GetAllFiltered(string token, BugSearchCriteria criteria)
        {
            Func<Bug, bool> matchesCriteria = criteria.MatchesCriteria;
            User userByToken = _userLogic.Get(token);
            return _bugRepository.GetAllFiltered(userByToken, matchesCriteria);
        }
    }
}