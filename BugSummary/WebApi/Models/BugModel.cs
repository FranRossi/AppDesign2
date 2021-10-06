using System.Collections.Generic;
using System.Linq;
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
        public int Id { get; set; }

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

        public Bug ToEntityWithID(int id)
        {
            return new()
            {
                Id = id,
                Name = Name,
                Description = Description,
                State = State,
                Version = Version,
                ProjectId = ProjectId
            };
        }
        public static BugModel ToModel(Bug bugEntity)
        {
            return new()
            {
                Id = bugEntity.Id,
                Name = bugEntity.Name,
                Description = bugEntity.Description,
                State = bugEntity.State,
                Version = bugEntity.Version,
                ProjectId = bugEntity.ProjectId
            };
        }

        public static IEnumerable<BugModel> ToModelList(IEnumerable<Bug> bugsToModel)
        {
            IEnumerable<BugModel> models = new List<BugModel>();
            foreach (var bug in bugsToModel)
            {
                models = models.Append(ToModel(bug));
            }
            return models;
        }
    }
}