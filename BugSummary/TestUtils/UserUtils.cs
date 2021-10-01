using Domain;
using System;

namespace TestUtils
{
    public class UserUtils
    {
        public bool CompareByField(User expectedUser, User actualUser)
        {
            bool result = true;
            result &= expectedUser.Id == expectedUser.Id;
            result &= expectedUser.FirstName == expectedUser.FirstName;
            result &= expectedUser.LastName == expectedUser.LastName;
            result &= expectedUser.Password == expectedUser.Password;
            result &= expectedUser.Email == expectedUser.Email;
            result &= expectedUser.Role == expectedUser.Role;
            return result;
        }
    }
}
