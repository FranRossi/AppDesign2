using ExternalReader;
using ExternalReaderImporterInterface;
using FileHandlerFactory;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Utilities.CustomExceptions;

namespace ExternalReaderImporter
{
    public class BugReaderImporterLogic : IBugReaderImporter
    {
        private string _pathToFolder;
        public ReaderFactory _readerFactory;

        public BugReaderImporterLogic(string pathToFolder)
        {
            this._pathToFolder = pathToFolder;
            _readerFactory = new ReaderFactory();
        }

        public IExternalReader GetExternalReader(string name)
        {
            IExternalReader provider = null;
            try
            {
                string fullPath = _pathToFolder + name + ".dll";
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
            List<Tuple<string, IEnumerable<Parameter>>> availableImporters = GetInternalReadersInfo();
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
                        string fileExtension = dllFile.Extension;
                        string name = dllFile.Name.Substring(0, dllFile.Name.Length - fileExtension.Length);
                        availableImporters.Add
                        (
                            Tuple.Create(name, parameters)
                        );
                    }

                }
            }
            return availableImporters;
        }

        private List<Tuple<string, IEnumerable<Parameter>>> GetInternalReadersInfo()
        {
            List<Tuple<string, IEnumerable<Parameter>>> availableImporters = new List<Tuple<string, IEnumerable<Parameter>>>();
            IEnumerable<string> companyReaderNames = _readerFactory.GetCompanyReaderNames();
            foreach (string companyReaderName in companyReaderNames)
            {
                Parameter pathParam = new Parameter
                {
                    Name = "Path",
                    Type = ParameterType.String
                };
                IEnumerable<Parameter> parameters = new List<Parameter> { pathParam };
                availableImporters.Add(Tuple.Create(companyReaderName, parameters));
            }
            return availableImporters;
        }
    }
}
