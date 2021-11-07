using ExternalReader;
using ExternalReaderImporterInterface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace ExternalReaderImporter
{
    public class ExternalReaderImporterLogic : IExternalReaderImporter
    {
        public IEnumerable<Tuple<string, IEnumerable<Parameter>>> GetExternalReadersInfo()
        {
            List<Tuple<string, IEnumerable<Parameter>>> availableImporters = new List<Tuple<string, IEnumerable<Parameter>>>();
            string[] filePaths = Directory.GetFiles("D:\\Github projects\\240503-219401\\BugSummary\\ExternalReaderAssemblies");
            foreach (string filePath in filePaths)
            {
                FileInfo dllFile = new FileInfo(filePath);
                Assembly assembly = Assembly.LoadFile(dllFile.FullName);
                foreach (Type type in assembly.GetTypes())
                {
                    if (typeof(IExternalReader).IsAssignableFrom(type))
                    {
                        IExternalReader provider = (IExternalReader)Activator.CreateInstance(type);
                        IEnumerable<Parameter> parameters = provider.GetParameters();
                        availableImporters.Add
                        (
                            Tuple.Create(dllFile.Name, parameters)
                        );
                    }

                }
            }
            return availableImporters;
        }
    }
}
