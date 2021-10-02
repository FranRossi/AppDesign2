using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicInterface
{
    public interface IBugLogic
    {
        void Add(User tester, Bug newBug);
    }
}
