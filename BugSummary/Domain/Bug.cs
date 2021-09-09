﻿using Utilities;

namespace Domain
{
    public class Bug
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string version { get; set; }
        public BugState state { get; set; }
    }
}