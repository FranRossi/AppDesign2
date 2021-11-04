using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalReader
{
    public class ParameterException : ExternalReaderException
    {
        public ParameterException()
        {
            messageToDisplay = "Invalid parameters, please check types and make sure not to leave any empty fields.";
        }
    }
}
