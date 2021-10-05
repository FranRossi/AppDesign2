using System.Collections.Generic;
using Domain;

namespace BusinessLogicInterface
{
    public interface IBugLogic
    {
        void Add(string token, Bug newBug);
        IEnumerable<Bug> GetAll(string token);
        void Update(string token, Bug updatedBug);
        void Delete(string token, int bugId);
        void FixBug(string token, int bugId);
    }
}