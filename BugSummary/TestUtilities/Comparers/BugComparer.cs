using Domain;

namespace TestUtilities.Comparers
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