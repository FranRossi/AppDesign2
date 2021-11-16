using System.Collections.Generic;
using System.Linq;
using CustomExceptions;
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
        public int FixingTime { get; set; }
        public string ProjectName { get; set; }
        public string FixerName { get; set; }

        public Bug ToEntity()
        {
            ValidateFields();
            return new Bug
            {
                Name = Name,
                Description = Description,
                State = State,
                Version = Version,
                ProjectId = ProjectId,
                FixingTime = FixingTime
            };
        }

        public Bug ToEntityWithID(int id)
        {
            ValidateFields();
            return new Bug
            {
                Id = id,
                Name = Name,
                Description = Description,
                State = State,
                Version = Version,
                ProjectId = ProjectId,
                FixingTime = FixingTime
            };
        }

        public static BugModel ToModel(Bug bugEntity)
        {
            return new BugModel
            {
                Id = bugEntity.Id,
                Name = bugEntity.Name,
                Description = bugEntity.Description,
                State = bugEntity.State,
                Version = bugEntity.Version,
                ProjectId = bugEntity.ProjectId,
                FixingTime = bugEntity.FixingTime,
                ProjectName = bugEntity.Project.Name,
                FixerName = bugEntity.Fixer.UserName
            };
        }

        public static IEnumerable<BugModel> ToModelList(IEnumerable<Bug> bugsToModel)
        {
            List<BugModel> models = new List<BugModel>();
            foreach (var bug in bugsToModel)
            {
                models.Add(ToModel(bug));
            }
            return models;
        }

        private void ValidateFields()
        {
            bool allFieldsExist = Id > 0;
            allFieldsExist &= Name != null;
            allFieldsExist &= Description != null;
            allFieldsExist &= State == BugState.Active || State == BugState.Fixed;
            allFieldsExist &= Version != null;
            allFieldsExist &= ProjectId > 0;
            if (!allFieldsExist)
                throw new BugModelMissingFieldException();
        }
    }
}