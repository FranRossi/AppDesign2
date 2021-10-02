using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
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

        public static bool CorrectBugState(BugState value)
        {
            return (value == BugState.Active || value == BugState.Inactive);
        }

        public static bool CheckValueIsNull(string value)
        {
            return value == null;
        }

        public static bool ValidateEmailFormat(string email)
        {
            if (!MailAddress.TryCreate(email, out var mailAddress))
                return false;
            return true;
        }
    }
}
