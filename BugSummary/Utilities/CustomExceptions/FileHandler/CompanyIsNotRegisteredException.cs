using System;

namespace Utilities.CustomExceptions.FileHandler
{
    public class CompanyIsNotRegisteredException : Exception
    {
        public override string Message => "The entered company is not registered on the API.";
    }
}