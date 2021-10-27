using Domain;

namespace DataAccess
{
    public class AssignmentRepository : BaseRepository<Assignment>
    {
        public AssignmentRepository(BugSummaryContext bugSummaryContext)
        {
            Context = bugSummaryContext;
        }
    }
}