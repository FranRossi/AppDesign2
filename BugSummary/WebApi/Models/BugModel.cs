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

        public Bug ToEntity() => new Bug()
        {
            Name = this.Name,
            Description = this.Description,
            State = this.State,
            Version = this.Version,
            ProjectId = this.ProjectId,
        };
    }
}