using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalReader
{
    public interface IExternalReader
    {
        IEnumerable<Parameter> GetParameters();
        IEnumerable<ProjectModel> GetProjectsFromFile(IEnumerable<Parameter> parameters);

    }
}
