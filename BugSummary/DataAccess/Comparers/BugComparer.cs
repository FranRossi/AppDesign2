using Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Comparers
{
    public class BugComparer : IComparer

    {
        public int Compare(object x, object y)
        {
            var expected = x as Bug;
            var actual = y as Bug;
            bool equals = this.ConcreteCompare(expected, actual);

            return equals ? 0 : 1;
        }
        protected bool ConcreteCompare(Bug expected, Bug actual)
        {
            bool equalsBug = expected.Name == actual.Name;
            equalsBug &= expected.Id == actual.Id;

            return equalsBug;
        }
    }
}
