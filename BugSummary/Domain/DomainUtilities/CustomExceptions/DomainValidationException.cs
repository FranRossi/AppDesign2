using System;

namespace Domain.DomainUtilities.CustomExceptions
{
    public abstract class DomainValidationException  : Exception
    {
        protected string messageToDisplay;
        public override string Message => this.messageToDisplay;
    }
}