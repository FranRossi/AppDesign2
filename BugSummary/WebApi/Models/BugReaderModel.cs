using ExternalReader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class BugReaderModel
    {
        public string Path { get; set; }
        public IEnumerable<Parameter> Parameters { get; set; }
    }
}
