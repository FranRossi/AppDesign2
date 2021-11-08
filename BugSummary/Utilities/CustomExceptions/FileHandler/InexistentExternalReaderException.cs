﻿using System;

namespace Utilities.CustomExceptions
{
    public class InexistentExternalReaderException : Exception
    {
        public override string Message => "The entered external reader file name does not exist, contact the admin to add it.";
    }
}