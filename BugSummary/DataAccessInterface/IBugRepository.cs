using System;
using System.Collections.Generic;
using Domain;

namespace DataAccessInterface
{
    public interface IBugRepository
    {
        Bug Get(User user, int bugId);

        void Add(User user, Bug newBug);

        IEnumerable<Bug> GetAllFiltered(User user, Func<Bug, bool> criteria);

        void Update(User user, Bug updatedBug);

        void Delete(User testerUser, int bugId);

        void Fix(User user, int bugId, int fixingTime);

        void Save();

    }
}