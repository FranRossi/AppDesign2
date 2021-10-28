using Domain;

namespace Utilities.Comparers
{
    public class AssignmentComparer : BaseComparer<Assignment>
    {
        protected override bool ConcreteCompare(Assignment expected, Assignment actual)
        {
            bool equalsBug = expected.Id == actual.Id;

            return equalsBug;
        }
    }
}