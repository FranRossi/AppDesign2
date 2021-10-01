
using Domain.DomainUtilities;
using Domain.DomainUtilities.CustomExceptions;

namespace Domain
{
    public class Bug
    {
        public int Id { get; set; }
        public string Name
        { 
                get => _name;
                set
                {
                    ValidateName(value);
                    _name = value;
                }
        }
        public string Description { get; set; }
        public string Version { get; set; }
        public BugState State { get; set; }
        public Project Project { get; set; }
        public int ProjectId { get; set; }

        private string _name;
        public const int MaxBugNameLength = 60;


        private void ValidateName(string nameToValidate)
        {
            if (!Validator.MaxLengthOfString(nameToValidate, MaxBugNameLength))
                throw new NameLengthIncorrectException();
        }
    }
}