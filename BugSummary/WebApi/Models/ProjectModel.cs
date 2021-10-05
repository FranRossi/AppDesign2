using Domain;

namespace WebApi.Models
{
    public class ProjectModel
    {
        public string Name { get; set; }

        public Project ToEntity()
        {
            return new()
            {
                Name = Name
            };
        }
    }
}