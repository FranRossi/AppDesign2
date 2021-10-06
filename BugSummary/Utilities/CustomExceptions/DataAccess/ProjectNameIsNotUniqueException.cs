﻿using System;

namespace Utilities.CustomExceptions
{
    public class ProjectNameIsNotUniqueException : DataAccessException
    {
        public ProjectNameIsNotUniqueException()
        {
            this.messageToDisplay = "The entered bug does not exist.";
        }
    }
}