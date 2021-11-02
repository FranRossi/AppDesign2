using Domain;
using System.Collections.Generic;
using System.Linq;

namespace WebApi.Models
{
    public class ProjectModel
    {
        public string Name { get; set; }
        public int BugCount { get; set; }
        public int Id { get; set; }
        public IEnumerable<BugModel> Bugs { get; set; }
        public IEnumerable<UserModel> Users { get; set; }

        public static IEnumerable<ProjectModel> ToModel(IEnumerable<Project> projects)
        {
            List<ProjectModel> modelList = new List<ProjectModel>();
            foreach (Project project in projects)
            {
                ProjectModel model = new ProjectModel();
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