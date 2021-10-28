using DataAccessInterface;
using Domain;

namespace DataAccess
{
    public class AssignmentRepository : BaseRepository<Assignment>, IAssignmentRepository
    {
        public AssignmentRepository(BugSummaryContext bugSummaryContext)
        {
            Context = bugSummaryContext;
        }

        public void Add(Assignment assignment)
        {
            Context.Assignments.Add(assignment);
        }
    }
}