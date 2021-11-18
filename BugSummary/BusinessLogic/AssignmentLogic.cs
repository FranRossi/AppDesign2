
using BusinessLogicInterface;
using DataAccessInterface;
using Domain;


namespace BusinessLogic
{
    public class AssignmentLogic : IAssignmentLogic
    {
        private IAssignmentRepository _assignmentRepository;
       
        public AssignmentLogic(IAssignmentRepository assignmentRepository)
        {
            _assignmentRepository = assignmentRepository;
        }

        public void Add(Assignment assignment)
        {
            _assignmentRepository.Add(assignment);
            _assignmentRepository.Save();
        }

    }
}