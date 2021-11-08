using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalReader
{
    public class WrongFormatException : ExternalReaderException
    {
        public WrongFormatException()
        {
            messageToDisplay = "Error trying to import bugs, check properties restrictions and file formatting.";
        }
    }
}
