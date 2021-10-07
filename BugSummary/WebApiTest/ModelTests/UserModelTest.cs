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
    public class UserModelTest
    {
        [TestMethod]
        public void UserToModelTest()
        {
            User expectedUser = new User
            {
                Id = 0,
                FirstName = "Pepe",
                LastName = "Perez",
                Password = "pepe1234",
                UserName = "pp",
                Email = "pepe@gmail.com",
                Role = RoleType.Tester
            };
            UserModel userToCompare = new UserModel
            {
                FirstName = "Pepe",
                LastName = "Perez",
                UserName = "pp",
                Email = "pepe@gmail.com",
                Role = RoleType.Tester,
            };
            UserModel model = UserModel.ToModel(expectedUser);
            CompareLogic compareLogic = new CompareLogic();
            ComparisonResult deepComparisonResult = compareLogic.Compare(userToCompare, model);
            Assert.IsTrue(deepComparisonResult.AreEqual);
        }

        [TestMethod]
        public void ModelToEntity()
        {
            User expectedUser = new User
            {
                FirstName = "Pepe",
                LastName = "Rodriquez",
                UserName = "pepito",
                Password = "pass1.4",
                Email = "pepe@gmail.com",
                Role = RoleType.Admin
            };
            UserModel userToCompare = new UserModel
            {
                FirstName = "Pepe",
                LastName = "Rodriquez",
                UserName = "pepito",
                Password = "pass1.4",
                Email = "pepe@gmail.com",
                Role = RoleType.Admin
            };
            User user = userToCompare.ToEntity();
            CompareLogic compareLogic = new CompareLogic();
            ComparisonResult deepComparisonResult = compareLogic.Compare(expectedUser, user);
            Assert.IsTrue(deepComparisonResult.AreEqual);
        }

        [TestMethod]
        public void InvalidModelToEntityNoFirstName()
        {
            UserModel userToCompare = new UserModel
            {
                LastName = "Rodriquez",
                UserName = "pepito",
                Password = "pass1.4",
                Email = "pepe@gmail.com",
                Role = RoleType.Admin
            };
            TestExceptionUtils.Throws<UserModelMissingFieldException>(
                () => userToCompare.ToEntity(), "Missing Fields: Required -> FirstName, LastName, UserName, Password, Email, Role."
            );
        }

        [TestMethod]
        public void InvalidModelToEntityNoLastName()
        {
            UserModel userToCompare = new UserModel
            {
                FirstName = "Pepe",
                UserName = "pepito",
                Password = "pass1.4",
                Email = "pepe@gmail.com",
                Role = RoleType.Admin
            };
            TestExceptionUtils.Throws<UserModelMissingFieldException>(
                () => userToCompare.ToEntity(), "Missing Fields: Required -> FirstName, LastName, UserName, Password, Email, Role."
            );
        }
        [TestMethod]
        public void InvalidModelToEntityNoUserName()
        {
            UserModel userToCompare = new UserModel
            {
                FirstName = "Pepe",
                LastName = "Rodriquez",
                Password = "pass1.4",
                Email = "pepe@gmail.com",
                Role = RoleType.Admin
            };
            TestExceptionUtils.Throws<UserModelMissingFieldException>(
                () => userToCompare.ToEntity(), "Missing Fields: Required -> FirstName, LastName, UserName, Password, Email, Role."
            );
        }
        [TestMethod]
        public void InvalidModelToEntityNoPassword()
        {
            UserModel userToCompare = new UserModel
            {
                FirstName = "Pepe",
                LastName = "Rodriquez",
                UserName = "pepito",
                Email = "pepe@gmail.com",
                Role = RoleType.Admin
            };
            TestExceptionUtils.Throws<UserModelMissingFieldException>(
                () => userToCompare.ToEntity(), "Missing Fields: Required -> FirstName, LastName, UserName, Password, Email, Role."
            );
        }
        [TestMethod]
        public void InvalidModelToEntityNoEmail()
        {
            UserModel userToCompare = new UserModel
            {
                FirstName = "Pepe",
                LastName = "Rodriquez",
                UserName = "pepito",
                Password = "pass1.4",
                Role = RoleType.Admin
            };
            TestExceptionUtils.Throws<UserModelMissingFieldException>(
                () => userToCompare.ToEntity(), "Missing Fields: Required -> FirstName, LastName, UserName, Password, Email, Role."
            );
        }
        [TestMethod]
        public void InvalidModelToEntityNoRole()
        {
            UserModel userToCompare = new UserModel
            {
                FirstName = "Pepe",
                LastName = "Rodriquez",
                UserName = "pepito",
                Password = "pass1.4",
                Email = "pepe@gmail.com",
            };
            TestExceptionUtils.Throws<UserModelMissingFieldException>(
                () => userToCompare.ToEntity(), "Missing Fields: Required -> FirstName, LastName, UserName, Password, Email, Role."
            );
        }

    }
}
