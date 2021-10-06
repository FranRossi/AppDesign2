using System;
using System.Collections.Generic;
using Domain;
using Utilities.Criterias;

namespace BusinessLogicInterface
{
    public interface IBugLogic
    {
        void Add(string token, Bug newBug);
        Bug Get(string token, int bugId);
        IEnumerable<Bug> GetAll(string token);
        IEnumerable<Bug> GetAllFiltered(string token, BugSearchCriteria criteria);
        void Update(string token, Bug updatedBug);
        void Delete(string token, int bugId);
        void FixBug(string token, int bugId);
    }
}