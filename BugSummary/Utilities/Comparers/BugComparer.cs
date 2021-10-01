using Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Comparers
{
    public class BugComparer : BaseComparer<Bug>
    {
        protected override bool ConcreteCompare(Bug expected, Bug actual)
        {
            bool equalsBug = expected.Name == actual.Name;
            equalsBug &= expected.Id == actual.Id;

            return equalsBug;
        }
    }
}
