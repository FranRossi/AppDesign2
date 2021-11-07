using ExternalReader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalReaderImporterInterface
{
    public interface IExternalReaderImporter
    {
        IEnumerable<Tuple<string, IEnumerable<Parameter>>> GetExternalReadersInfo();
        IExternalReader GetExternalReader(string path);
    }
}
