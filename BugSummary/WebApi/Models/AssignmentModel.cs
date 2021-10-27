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
        public int Duration { get; set; }
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

    }
}