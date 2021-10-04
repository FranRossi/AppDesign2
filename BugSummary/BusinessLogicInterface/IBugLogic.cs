using Domain;
using System;
using System.Collections.Generic;


namespace BusinessLogicInterface
{
    public interface IBugLogic
    {
        void Add(string token, Bug newBug);
        IEnumerable<Bug> GetAll(string token);
        void Update(string token, Bug updatedBug);
    }
}
