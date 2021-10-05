using Domain;

namespace Utilities.Comparers
{
    public class UserComparer : BaseComparer<User>
    {
        protected override bool ConcreteCompare(User expected, User actual)
        {
            var equalsUser = expected.Email == actual.Email;
            equalsUser &= expected.Id == actual.Id;

            return equalsUser;
        }
    }
}