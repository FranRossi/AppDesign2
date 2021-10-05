﻿using System.Collections.Generic;
using Domain;

namespace DataAccessInterface
{
    public interface IBugRepository
    {
        Bug Get(int bugId);
        void Add(User tester, Bug newBug);
        IEnumerable<Bug> GetAllByTester(User tester);
        IEnumerable<Bug> GetAll();
        void Update(User testerUser, Bug updatedBug);
        void Delete(User testerUser, int bugId);
        void Save();
        void FixBug(User developerUser, int bugId);
    }
}