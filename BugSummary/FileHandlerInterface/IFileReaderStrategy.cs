using ExternalReader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileHandlerInterface
{
    public interface IFileReaderStrategy
    {
        public IEnumerable<ProjectModel> GetProjectsFromFile(string path);
    }
}
