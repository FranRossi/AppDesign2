using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalReader
{
    public class UnableToReadException : ExternalReaderException
    {
        public UnableToReadException()
        {
            messageToDisplay = "Unable to read file, check path or read permissions.";
        }
    }
}
