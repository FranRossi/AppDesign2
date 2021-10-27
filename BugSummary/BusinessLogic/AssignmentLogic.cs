using System;
using System.Collections.Generic;
using BusinessLogicInterface;
using DataAccessInterface;
using Domain;
using Utilities.Criterias;


namespace BusinessLogic
{
    public class AssignmentLogic
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