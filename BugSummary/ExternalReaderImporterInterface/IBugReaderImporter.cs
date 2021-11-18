using ExternalReader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugReaderImporterInterface
{
    public interface IBugReaderImporter
    {
        IEnumerable<Tuple<string, IEnumerable<Parameter>>> GetExternalReadersInfo();
        IExternalReader GetExternalReader(string path);
    }
}
