using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalReader
{
    public class ProjectModel
    {
        public string Name { get; set; }
        public IEnumerable<BugModel> Bugs { get; set; }
    }
}
