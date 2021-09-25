using Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Comparers
{
    public class UserComparer : IComparer

    {
        public int Compare(object x, object y)
        {
            var expected = x as User;
            var actual = y as User;
            bool equals = this.ConcreteCompare(expected, actual);

            return equals ? 0 : 1;
        }
        protected bool ConcreteCompare(User expected, User actual)
        {
            bool equalsRestaurant = expected.Email == actual.Email;
            equalsRestaurant &= expected.Id == actual.Id;

            return equalsRestaurant;
        }
    }
}
