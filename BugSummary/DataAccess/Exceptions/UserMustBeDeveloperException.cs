using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Exceptions
{
    public class UserMustBeDeveloperException : Exception
    {
        public override string Message => "User's role must be developer to perform this action";
    }
}