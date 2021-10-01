
using Domain.DomainUtilities;

namespace Domain
{
    public class Bug
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Version { get; set; }
        public BugState State { get; set; }
        public Project ProjectId { get; set; }

    }
}