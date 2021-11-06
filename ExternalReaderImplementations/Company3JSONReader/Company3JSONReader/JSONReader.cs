using ExternalReader;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Company3JSONReader
{
    public class JSONReader : IExternalReader
    {
        public IEnumerable<Parameter> GetParameters()
        {
            return new List<Parameter> {
                new Parameter {
                    Name = "Path",
                    Type = ParameterType.String
                }
            };
        }

        public IEnumerable<ProjectModel> GetProjectsFromFile(IEnumerable<Parameter> parameters)
        {
            IEnumerable<ProjectModel> result;
            string path = GetPath(parameters);
            try
            {
                string json = System.IO.File.ReadAllText(path);
                ProjectModel project = JsonConvert.DeserializeObject<ProjectModel>(json);
                result = new List<ProjectModel>() { project };
            }
            catch (DirectoryNotFoundException)
            {
                throw new UnableToReadException();
            }

            return result;
        }

        private string GetPath(IEnumerable<Parameter> parameters)
        {
            string path = "";
            foreach (Parameter param in parameters)
            {
                if (param.Name == "Path")
                    path = param.Value;
            }
            if (path == "")
                throw new UnableToReadException();
            return path;
        }
    }
}
