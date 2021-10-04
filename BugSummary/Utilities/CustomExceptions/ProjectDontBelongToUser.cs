﻿using System;

namespace Utilities.CustomExceptions
{
    public class ProjectDontBelongToUser : Exception
    {
        public override string Message => "New project to update bug, does not belong to tester";
    }
}