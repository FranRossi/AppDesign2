using FileHandler;
using FileHandlerInterface;
using System;

namespace FileHandlerFactory
{
    public class ReaderFactory
    {
        public IFileReaderStrategy GetStrategy(string companyName)
        {
            return new Company1Reader();
        }
    }
}
