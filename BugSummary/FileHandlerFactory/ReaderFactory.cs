using FileHandler;
using FileHandlerInterface;
using System;
using System.Collections.Generic;
using Utilities.CustomExceptions;

namespace FileHandlerFactory
{
    public class ReaderFactory
    {
        private static Dictionary<string, IFileReaderStrategy> _commandMap = new Dictionary<string, IFileReaderStrategy>()
        {
            { "Company1", new Company1Reader() },
            { "Company2", new Company2Reader() }
        };
        public virtual IFileReaderStrategy GetStrategy(string companyName)
        {
            try
            {
                return _commandMap[companyName];
            }
            catch (KeyNotFoundException)
            {
                throw new CompanyIsNotRegisteredException();
            };
        }

        public IEnumerable<string> GetCompanyReaderNames()
        {
            return new List<string>(_commandMap.Keys);
        }
    }
}
