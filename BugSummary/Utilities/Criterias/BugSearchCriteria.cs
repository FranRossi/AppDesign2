using System.Runtime.InteropServices;
using Domain;
using Domain.DomainUtilities;

namespace Utilities.Criterias
{
    public class BugSearchCriteria
    {
        public string Name { get; set; }
        public BugState? State { get; set; }
        public int? ProjectId { get; set; }
        public int? Id { get; set; }

        public bool MatchesCriteria(Bug bug)
        {
            bool matches = (bug.Name == Name || Name == null) &&
                           (bug.State == State || State == null) &&
                           (bug.ProjectId == ProjectId || ProjectId == null) &&
                           (bug.Id == Id || Id == null);
            return matches;

        }
    }
}