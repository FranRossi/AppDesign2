using Domain.DomainUtilities;

namespace Utilities.Criterias
{
    public class BugSearchCriteria
    {
        public string Name { get; set; }
        public BugState State { get; set; }
        public int ProjectId { get; set; }
        public int Id { get; set; }
        
    }
}