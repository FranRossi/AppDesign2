using Domain.DomainUtilities;
using Domain.DomainUtilities.CustomExceptions;
using System;
using System.Collections.Generic;

namespace Domain
{
    public class User
    {
        private string _firstName;
        private string _lastName;
        private string _userName;
        private string _pass;
        private string _email;
        private RoleType _role;

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
        public List<Project> Projects { get; set; }
        public int ProjectId { get; set; }


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


    }
}