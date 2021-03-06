
using System.Collections.Generic;
using Domain.DomainUtilities;
using Domain.DomainUtilities.CustomExceptions;

namespace Domain
{
    public class User
    {
        private string _email;
        private string _firstName;
        private string _lastName;
        private string _pass;
        private RoleType _role;
        private string _userName;
        private int _hourlyRate;

        public int Id { get; set; }

        public string FirstName
        {
            get => _firstName;
            set
            {
                ValidateStringIsNotNull(value);
                _firstName = value;
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                ValidateStringIsNotNull(value);
                _lastName = value;
            }
        }

        public string UserName
        {
            get => _userName;
            set
            {
                ValidateStringIsNotNull(value);
                _userName = value;
            }
        }

        public string Password
        {
            get => _pass;
            set
            {
                ValidateStringIsNotNull(value);
                _pass = value;
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                ValidateEmail(value);
                _email = value;
            }
        }

        public RoleType Role
        {
            get => _role;
            set
            {
                ValidateRole(value);
                _role = value;
            }
        }

        public int HourlyRate
        {
            get => _hourlyRate;
            set
            {
                if (_role == RoleType.Admin)
                    _hourlyRate = 0;
                else
                {
                    ValidateHourlyRate(value);
                    _hourlyRate = value;
                }
            }
        }

        public string Token { get; set; }
        public List<Project> Projects { get; set; }
        public List<Bug> FixedBugs { get; set; }

        private void ValidateStringIsNotNull(string value)
        {
            if (Validator.CheckValueIsNull(value))
                throw new UserPropertyIsNullException();
        }

        private void ValidateEmail(string email)
        {
            if (!Validator.ValidateEmailFormat(email))
                throw new EmailIsIncorrectException();
        }

        private void ValidateRole(RoleType value)
        {
            if (!Validator.CorrectRole(value))
                throw new UserRoleIncorrectException();
        }

        private void ValidateHourlyRate(int value)
        {
            if (!Validator.ValidateGreaterThanZero(value))
                throw new InvalidUserHourlyRateException();
        }

        public int GetFixedBugCount()
        {
            if (FixedBugs == null)
                return 0;
            return FixedBugs.Count;
        }

    }
}