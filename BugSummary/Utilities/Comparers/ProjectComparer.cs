using Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Comparers
{
    public class ProjectComparer : BaseComparer<Project>
    {
        protected override bool ConcreteCompare(Project expected, Project actual)
        {
            bool equalsProject = expected.Name == actual.Name;
            equalsProject &= expected.Id == actual.Id;

            return equalsProject;
        }
    }
}
