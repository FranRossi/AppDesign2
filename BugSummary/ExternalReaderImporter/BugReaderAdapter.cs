using ExternalReader;
using FileHandlerInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugReaderImporter
{
    public class BugReaderAdapter : IExternalReader
    {
        private IFileReaderStrategy _internalReader;
        public BugReaderAdapter(IFileReaderStrategy internalReader)
        {
            _internalReader = internalReader;
        }

        public IEnumerable<Parameter> GetParameters()
        {
            Parameter pathParam = new Parameter
            {
                Name = "Path",
                Type = ParameterType.String
            };
            IEnumerable<Parameter> parameters = new List<Parameter> { pathParam };
            return parameters;
        }

        public IEnumerable<ProjectModel> GetProjectsFromFile(IEnumerable<Parameter> parameters)
        {
            string path = "";
            foreach (Parameter parameter in parameters)
            {
                if (parameter.Name == "Path")
                    path = parameter.Value;
            }
            if (path == "")
                throw new ParameterException();
            return _internalReader.GetProjectsFromFile(path);
        }
    }
}
