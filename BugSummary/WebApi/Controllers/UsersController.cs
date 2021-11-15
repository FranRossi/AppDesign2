using BusinessLogicInterface;
using Domain;
using Domain.DomainUtilities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using WebApi.Filters;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("users")]
    [AuthorizationWithParameterFilter(new[] { RoleType.Admin })]
    [ExceptionFilter]
    public class UsersController : ControllerBase
    {
        private readonly IUserLogic _users;

        public UsersController(IUserLogic users)
        {
            _users = users;
        }

        [HttpPost]
        public IActionResult Post([FromBody] UserModel model)
        {
            _users.Add(model.ToEntity());
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            int result = _users.GetFixedBugCount(id);
            return Ok(result);
        }

        public IActionResult Get([FromHeader] string token)
        {
            IEnumerable<Project> projects = _users.GetProjects(token);
            return Ok(ProjectModel.ToModelList(projects));
        }
    }
}