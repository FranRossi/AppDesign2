using Domain;
using Domain.DomainUtilities;

namespace WebApi.Models
{
    public class BugModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public BugState State { get; set; }
        public string Version { get; set; }
        public int ProjectId { get; set; }

        public Bug ToEntity()
        {
            return new()
            {
                Name = Name,
                Description = Description,
                State = State,
                Version = Version,
                ProjectId = ProjectId
            };
        }
    }
}