using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalReader
{
    public class BugModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Version { get; set; }
        public BugState State { get; set; }
    }

    public enum BugState
    {
        Fixed = 1,
        Active = 2
    }
}
