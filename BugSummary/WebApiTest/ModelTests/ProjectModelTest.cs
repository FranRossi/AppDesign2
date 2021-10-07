using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BusinessLogicInterface;
using CustomExceptions;
using Domain;
using Domain.DomainUtilities;
using KellermanSoftware.CompareNetObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TestUtilities;
using Utilities.Comparers;
using Utilities.Criterias;
using WebApi.Controllers;
using WebApi.Models;

namespace WebApiTest
{

    [TestClass]
    public class ProjectModelTest
    {
        [TestMethod]
        public void InvalidModelToEntityNoId()
        {
            ProjectModel bugToCompare = new ProjectModel
            {
            };
            TestExceptionUtils.Throws<ProjectModelMissingFieldException>(
                () => bugToCompare.ToEntity(), "Missing Fields: Required -> Name."
            );
        }
    }
}
