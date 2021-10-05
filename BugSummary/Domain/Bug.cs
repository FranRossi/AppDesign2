using Domain.DomainUtilities;
using Domain.DomainUtilities.CustomExceptions;

namespace Domain
{
    public class Bug
    {
        public const int MaxBugNameLength = 60;
        public const int MaxBugDescriptionLength = 150;
        public const int MaxBugIdLength = 4;
        public const int MaxBugVersionLength = 10;
        private string _description;
        private int _id;

        private string _name;
        private BugState _state;
        private string _version;


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

        public string Description
        {
            get => _description;
            set
            {
                ValidateDescription(value);
                _description = value;
            }
        }

        public string Version
        {
            get => _version;
            set
            {
                ValidateVersion(value);
                _version = value;
            }
        }


        public BugState State
        {
            get => _state;
            set
            {
                ValidateState(value);
                _state = value;
            }
        }

        public Project Project { get; set; }
        public int ProjectId { get; set; }


        private void ValidateName(string nameToValidate)
        {
            if (!Validator.MaxLengthOfString(nameToValidate, MaxBugNameLength))
                throw new BugNameLengthIncorrectException();
        }

        public void ValidateIdLength(int idToValidate)
        {
            if (!Validator.MaxLengthOfString(idToValidate.ToString(), MaxBugIdLength))
                throw new BugIdLengthIncorrectException();
        }

        private void ValidateDescription(string descriptionToValidate)
        {
            if (!Validator.MaxLengthOfString(descriptionToValidate, MaxBugDescriptionLength))
                throw new BugDescriptionLengthIncorrectException();
        }

        private void ValidateVersion(string versionToValidate)
        {
            if (!Validator.MaxLengthOfString(versionToValidate, MaxBugVersionLength))
                throw new BugVersionLengthIncorrectException();
        }

        private void ValidateState(BugState value)
        {
            if (!Validator.CorrectBugState(value))
                throw new BugStateIncorrectException();
        }
    }
}