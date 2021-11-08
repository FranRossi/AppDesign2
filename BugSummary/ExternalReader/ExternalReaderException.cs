using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalReader
{
    public class ExternalReaderException : Exception
    {
        protected string messageToDisplay;
        public override string Message => messageToDisplay;
    }
}
