﻿using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Utilities;

namespace DomainTest
{

    [TestClass]
    public class ProjectTest
    {
        [TestMethod]
        public void CreateId()
        {
            Project newProject = new Project
            {
                Id = 154
            };
            Assert.AreEqual(154, newProject.Id);
        }

        [TestMethod]
        public void CreateName()
        {
            Project newProject = new Project
            {
                Name = "Semester2021"
            };
            Assert.AreEqual(154, newProject.Name);
        }
    }


}
