using Domain.DomainUtilities;
using Domain.DomainUtilities.CustomExceptions;

namespace Domain
{
    public class Assignment
    {
        public const int MaxAssignmentNameLength = 60;
        public const int MaxAssignmentIdLength = 4;
        
        
        private string _name;
        private int _id;

        public int Id
        {
            get => _id;
            set
            {
                ValidateIdLength(value);
                _id = value;
            }
        }
        public string Name
        {
            get => _name;
            set
            {
                ValidateName(value);
                _name = value;
            }
        }
        public int HourlyRate { get; set; }
        public double Duration { get; set; }
        public Project Project { get; set; }
        public int ProjectId { get; set; }
        
        
        private void ValidateName(string nameToValidate)
        {
            if (!Validator.MaxLengthOfString(nameToValidate, MaxAssignmentNameLength))
                throw new AssignmentNameLengthIncorrectException();
        }
        
        public void ValidateIdLength(int idToValidate)
        {
            if (!Validator.MaxLengthOfString(idToValidate.ToString(), MaxAssignmentIdLength))
                throw new AssignmentIdLengthIncorrectException();
        }
    }
}