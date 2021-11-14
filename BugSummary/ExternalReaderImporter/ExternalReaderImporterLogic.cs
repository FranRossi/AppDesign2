using ExternalReader;
using ExternalReaderImporterInterface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Utilities.CustomExceptions;

namespace ExternalReaderImporter
{
    public class ExternalReaderImporterLogic : IExternalReaderImporter
    {
        private string _pathToFolder;

        public ExternalReaderImporterLogic(string pathToFolder)
        {
            this._pathToFolder = pathToFolder;
        }

        public IExternalReader GetExternalReader(string name)
        {
            IExternalReader provider = null;
            try
            {
                string fullPath = _pathToFolder + name;
                FileInfo dllFile = new FileInfo(fullPath);
                Assembly assembly = Assembly.LoadFile(dllFile.FullName);
                foreach (Type type in assembly.GetTypes())
                    if (typeof(IExternalReader).IsAssignableFrom(type))
                        provider = (IExternalReader)Activator.CreateInstance(type);
                return provider;
            }
            catch (FileNotFoundException)
            {
                throw new InexistentExternalReaderException();
            }
        }

        public IEnumerable<Tuple<string, IEnumerable<Parameter>>> GetExternalReadersInfo()
        {
            List<Tuple<string, IEnumerable<Parameter>>> availableImporters = new List<Tuple<string, IEnumerable<Parameter>>>();
            string[] filePaths = Directory.GetFiles(_pathToFolder);
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
