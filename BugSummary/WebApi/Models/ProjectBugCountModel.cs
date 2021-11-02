using Domain;
using System.Collections.Generic;
using System.Linq;

namespace WebApi.Models
{
    public class ProjectBugCountModel
    {
        public string Name { get; set; }
        public int BugCount { get; set; }
        public int Id { get; set; }
        public IEnumerable<BugModel> Bugs { get; set; }
        public IEnumerable<UserModel> Users { get; set; }

        public static IEnumerable<ProjectBugCountModel> ToModel(IEnumerable<Project> projects)
        {
            List<ProjectBugCountModel> modelList = new List<ProjectBugCountModel>();
            foreach (Project project in projects)
            {
                ProjectBugCountModel model = new ProjectBugCountModel();
                model.Id = project.Id;
                model.Name = project.Name;
                model.BugCount = project.Bugs.Count;
                model.Bugs = BugModel.ToModelList(project.Bugs);
                model.Users = ToModelListUsers(project.Users);
                modelList.Add(model);
            }

            return modelList;
        }
        
        private static IEnumerable<UserModel> ToModelListUsers(IEnumerable<User> usersToModel)
        {
            List<UserModel> models = new List<UserModel>();
            foreach (User user in usersToModel)
            {
                models.Add(UserModel.ToModel(user));
            }
            return models;
        } 
    }
}