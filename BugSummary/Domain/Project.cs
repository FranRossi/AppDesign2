using System.Collections.Generic;

namespace Domain
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Bug> Bugs { get; set; }
    }
}