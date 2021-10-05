using System.Net.Mail;

namespace Domain.DomainUtilities
{
    public static class Validator
    {
        public static bool MaxLengthOfString(string text, int maxNumber)
        {
            return text.Length <= maxNumber;
        }

        public static bool CorrectBugState(BugState value)
        {
            return value == BugState.Active || value == BugState.Done;
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

        public static bool CorrectRole(RoleType value)
        {
            return value is RoleType.Admin or RoleType.Developer or RoleType.Tester;
        }

        public static bool CorrectFixerRole(RoleType value)
        {
            return value == RoleType.Developer;
        }
    }
}