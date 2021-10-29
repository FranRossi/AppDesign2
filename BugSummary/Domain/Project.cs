using System;
using System.Collections.Generic;
using Domain.DomainUtilities;
using Domain.DomainUtilities.CustomExceptions;

namespace Domain
{
    public class Project
    {
        public const int MaxProjectNameLength = 30;

        private string _name;

        public int Id { get; set; }

        public string Name
        {
            get => _name;
            set
            {
                ValidateName(value);
                _name = value;
            }
        }

        public List<Bug> Bugs { get; set; }
        public List<User> Users { get; set; }
        public List<Assignment> Assignments { get; set; }

        private void ValidateName(string nameToValidate)
        {
            if (!Validator.MaxLengthOfString(nameToValidate, MaxProjectNameLength))
                throw new ProjectNameLengthIncorrectException();
        }

        public void AddUser(User newUser)
        {
            if (newUser.Role == RoleType.Developer || newUser.Role == RoleType.Tester)
            {
                if (Users == null)
                    Users = new List<User>();
                Users.Add(newUser);
            }
            else
            {
                throw new InvalidProjectAssigneeRoleException();
            }
        }

        public void RemoveUser(User newUser)
        {
            if (Users != null)
                Users.Remove(newUser);
        }

        public int CalculateDuration()
        {
            int duration = 0;
            if (Bugs != null)
                foreach (Bug bug in Bugs)
                    duration += bug.FixingTime;
            if (Assignments != null)
                foreach (Assignment assignment in Assignments)
                    duration += (int)assignment.Duration;
            return duration;
        }

        public int CalculateCost()
        {
            int cost = 0;
            if (Bugs != null)
                foreach (Bug bug in Bugs)
                    cost += bug.FixingTime * bug.GetFixerHourlyRate();
            if (Assignments != null)
                foreach (Assignment assignment in Assignments)
                    cost += (int)assignment.Duration * assignment.HourlyRate;
            return cost;
        }
    }
}