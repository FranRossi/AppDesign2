using System;

namespace Utilities.CustomExceptions.DataAccess
{
    public class DataAccessException : Exception
    {
        protected string MessageToDisplay;
        public override string Message => this.MessageToDisplay;
    }
}
