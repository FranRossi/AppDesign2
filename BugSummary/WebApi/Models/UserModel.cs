using System.Text.Json.Serialization;
using CustomExceptions;
using Domain;
using Domain.DomainUtilities;

namespace WebApi.Models
{
    public class UserModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { internal get; set; }
        public string Email { get; set; }
        public RoleType Role { get; set; }
        public int HourlyRate { get; set; }

        public User ToEntity()
        {
            ValidateFields();
            return new()
            {
                FirstName = FirstName,
                LastName = LastName,
                UserName = UserName,
                Password = Password,
                Email = Email,
                Role = Role,
                HourlyRate = HourlyRate
            };
        }

        public static UserModel ToModel(User user)
        {
            UserModel model = new UserModel();
            model.FirstName = user.FirstName;
            model.LastName = user.LastName;
            model.Email = user.Email;
            model.UserName = user.UserName;
            model.Role = user.Role;
            model.HourlyRate = user.HourlyRate;

            return model;
        }

        private void ValidateFields()
        {
            bool allFieldsExist = FirstName != null;
            allFieldsExist &= LastName != null;
            allFieldsExist &= Email != null;
            allFieldsExist &= Password != null;
            allFieldsExist &= UserName != null;
            allFieldsExist &= Role == RoleType.Admin || Role == RoleType.Tester || Role == RoleType.Developer;
            if (!allFieldsExist)
                throw new UserModelMissingFieldException();
        }
    }
}