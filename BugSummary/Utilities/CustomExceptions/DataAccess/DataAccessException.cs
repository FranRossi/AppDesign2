using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.CustomExceptions
{
    public class DataAccessException : Exception
    {
        protected string messageToDisplay;
        public override string Message => this.messageToDisplay;
    }
}
