using FileHandler;
using FileHandlerInterface;
using System;

namespace FileHandlerFactory
{
    public class ReaderFactory
    {
        public virtual IFileReaderStrategy GetStrategy(string companyName)
        {
            return new Company1Reader();
        }
    }
}
