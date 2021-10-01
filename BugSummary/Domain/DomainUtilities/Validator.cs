using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DomainUtilities
{
    public static class Validator
    {
        public static bool MaxLengthOfString(string text, int maxNumber)
        {
            return (text.Length <= maxNumber);
        }
    }
}
