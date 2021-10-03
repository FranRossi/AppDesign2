using Domain;
using System;
using System.Collections.Generic;


namespace BusinessLogicInterface
{
    public interface IBugLogic
    {
        void Add(User tester, Bug newBug);
        IEnumerable<Bug> GetAll(string token);
    }
}
