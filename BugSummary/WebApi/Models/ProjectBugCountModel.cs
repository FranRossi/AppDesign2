using Domain;
using System.Collections.Generic;

namespace WebApi.Models
{
    public class ProjectBugCountModel
    {
        public string Name { get; set; }
        public int BugCount { get; set; }
        public int Cost { get; set; }
        public int Duration { get; set; }

        public static IEnumerable<ProjectBugCountModel> ToModel(IEnumerable<Project> projects)
        {
            List<ProjectBugCountModel> modelList = new List<ProjectBugCountModel>();
            foreach (Project project in projects)
            {
                ProjectBugCountModel model = new ProjectBugCountModel();
                model.Name = project.Name;
                model.BugCount = project.Bugs.Count;
                model.Cost = project.CalculateCost();
                model.Duration = project.CalculateDuration();
                modelList.Add(model);
            }

            return modelList;
        }
    }
}