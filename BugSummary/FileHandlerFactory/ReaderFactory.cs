using FileHandler;
using FileHandlerInterface;
using System;
using System.Collections.Generic;

namespace FileHandlerFactory
{
    public class ReaderFactory
    {
        private static Dictionary<string, IFileReaderStrategy> _commandMap = new Dictionary<string, IFileReaderStrategy>()
        {
            { "Empresa1", new Company1Reader() },
            { "Empresa2", new Company2Reader() }
        };
        public virtual IFileReaderStrategy GetStrategy(string companyName)
        {
            return _commandMap[companyName];
        }
    }
}
