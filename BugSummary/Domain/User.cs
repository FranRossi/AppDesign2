using Domain.DomainUtilities;
using Domain.DomainUtilities.CustomExceptions;
using System;

namespace Domain
{
    public class User
    {
        private string _firstName;
        private string _lastName;
        private string _userName;
        private string _pass;

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
        public string Email { get; set; }
        public RoleType Role { get; set; }
        public Project Project { get; set; }
        public int ProjectId { get; set; }


        private void ValidateStringIsNotNull(string value)
        {
            if (Validator.CheckValueIsNull(value))
                throw new UserPropertyIsNullException();
        }

    }
}