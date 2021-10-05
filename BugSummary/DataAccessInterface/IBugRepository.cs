using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterface
{
    public interface IBugRepository
    {
        void Add(User tester, Bug newBug);
        IEnumerable<Bug> GetAllByTester(User tester);
        IEnumerable<Bug> GetAll();
        void Update(User testerUser, Bug updatedBug);
        void Save();
        void FixBug(User developerUser, int bugId);
    }
}
