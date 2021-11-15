using System.Collections.Generic;
using System.Linq;
using CustomExceptions;
using Domain;
using Domain.DomainUtilities;

namespace WebApi.Models
{
    public class AssignmentModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Duration { get; set; }
        public int HourlyRate { get; set; }
        public int ProjectId { get; set; }


        public Assignment ToEntity()
        {
            ValidateFields();
            return new()
            {
                Name = Name,
                Duration = Duration,
                HourlyRate = HourlyRate,
                ProjectId = ProjectId
            };
        }

        
        
        private void ValidateFields()
        {
            bool allFieldsExist = Id > 0;
            allFieldsExist &= Name != null;
            allFieldsExist &= Duration > 0;
            allFieldsExist &= HourlyRate > 0;
            allFieldsExist &= ProjectId > 0;
            if (!allFieldsExist)
                throw new AssignmentModelMissingFieldException();
        }

        public static AssignmentModel ToModel(Assignment assignment)
        {
            return new AssignmentModel
            {
                Id = assignment.Id,
                Name = assignment.Name,
                HourlyRate = assignment.HourlyRate,
                Duration = assignment.Duration,
                ProjectId = assignment.ProjectId
            };
        }

        public static IEnumerable<AssignmentModel> ToModelList(List<Assignment> assignmentsToModel)
        {
            List<AssignmentModel> models = new List<AssignmentModel>();
            foreach (var assignment in assignmentsToModel)
            {
                models.Add(ToModel(assignment));
            }
            return models;
        }
    }
}