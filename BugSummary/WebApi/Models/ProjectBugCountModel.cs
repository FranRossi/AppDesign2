using Domain;

namespace WebApi.Models
{
    public class ProjectBugCountModel
    {
        public string Name { get; set; }
        public int BugCount { get; set; }

        public static ProjectBugCountModel ToModel(Project project)
        {
            ProjectBugCountModel model = new ProjectBugCountModel();
            model.Name = project.Name;
            model.BugCount = project.Bugs.Count;

            return model;
        }
    }
}