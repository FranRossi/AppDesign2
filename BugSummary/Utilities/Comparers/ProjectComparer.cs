using Domain;

namespace Utilities.Comparers
{
    public class ProjectComparer : BaseComparer<Project>
    {
        protected override bool ConcreteCompare(Project expected, Project actual)
        {
            var equalsProject = expected.Name == actual.Name;
            equalsProject &= expected.Id == actual.Id;

            return equalsProject;
        }
    }
}