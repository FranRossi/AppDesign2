using Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Comparers
{
    public class UserComparer : BaseComparer<User>

    {
        protected override bool ConcreteCompare(User expected, User actual)
        {
            bool equalsUser = expected.Email == actual.Email;
            equalsUser &= expected.Id == actual.Id;

            return equalsUser;
        }
    }
}
