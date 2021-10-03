﻿using BusinessLogicInterface;
using DataAccessInterface;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class ProjectLogic : IProjectLogic
    {
        private IProjectRepository _projectRepository;
        public ProjectLogic(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public void Add(Project newProject)
        {
            _projectRepository.Add(newProject);
            _projectRepository.Save();
        }

        public void Update(int id, Project updatedProject)
        {
            updatedProject.Id = id;
            _projectRepository.Update(updatedProject);
            _projectRepository.Save();
        }
    }
}
