using System;
using System.Collections.Generic;
using Domain;

namespace DataAccessInterface
{
    public interface IBugRepository
    {
        Bug Get(User user, int bugId);
        void Add(User tester, Bug newBug);
        IEnumerable<Bug> GetAllFiltered(User user,Func<Bug,bool> criteria);
        IEnumerable<Bug> GetAllByUser(User user);
        void Update(User testerUser, Bug updatedBug);
        void Delete(User testerUser, int bugId);
        void Save();
        void FixBug(User developerUser, int bugId);
    }
}