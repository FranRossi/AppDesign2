using System.Collections.Generic;
using BusinessLogicInterface;
using DataAccessInterface;
using Domain;

namespace BusinessLogic
{
    public class BugLogic : IBugLogic
    {
        private readonly IBugRepository _bugRepository;
        private readonly IUserRepository _userRepository;

        public BugLogic(IBugRepository bugRepository, IUserRepository userRepository)
        {
            _bugRepository = bugRepository;
            _userRepository = userRepository;
        }

        public void Add(string token, Bug newBug)
        {
            var userLogic = new UserLogic(_userRepository);
            var userByToken = userLogic.Get(token);
            _bugRepository.Add(userByToken, newBug);
            _bugRepository.Save();
        }

        public IEnumerable<Bug> GetAll(string token)
        {
            var userLogic = new UserLogic(_userRepository);
            var userByToken = userLogic.Get(token);
            return _bugRepository.GetAllByTester(userByToken);
        }

        public void Update(string token, Bug updatedBug)
        {
            var userLogic = new UserLogic(_userRepository);
            var userByToken = userLogic.Get(token);
            _bugRepository.Update(userByToken, updatedBug);
            _bugRepository.Save();
        }

        public void Delete(string token, int bugId)
        {
            var userLogic = new UserLogic(_userRepository);
            var userByToken = userLogic.Get(token);
            _bugRepository.Delete(userByToken, bugId);
            _bugRepository.Save();
        }
    }
}