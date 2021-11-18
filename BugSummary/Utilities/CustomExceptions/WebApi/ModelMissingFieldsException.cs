using System;

namespace Utilities.CustomExceptions.WebApi
{
    public class ModelMissingFieldsException : Exception
    {
        protected string MessageToDisplay;
        public override string Message => this.MessageToDisplay;
    }
}
