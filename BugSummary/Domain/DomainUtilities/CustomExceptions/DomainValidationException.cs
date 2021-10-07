using System;

namespace Domain.DomainUtilities.CustomExceptions
{
    public class DomainValidationException : Exception
    {
        protected string messageToDisplay;
        public override string Message => messageToDisplay;
    }
}