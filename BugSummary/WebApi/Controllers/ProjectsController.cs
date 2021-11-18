using BusinessLogicInterface;
using Domain;
using Domain.DomainUtilities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using Microsoft.OpenApi.Any;
using WebApi.Filters;
using WebApi.Models;

namespace WebApi.Controllers
{
    [EnableCors("CorsApi")]
    [ApiController]
    [Route("projects")]
    [ExceptionFilter]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectLogic _projects;
        private readonly IUserLogic _users;

        public ProjectsController(IProjectLogic projects, IUserLogic users)
        {
            _projects = projects;
            _users = users;
        }

        [AuthorizationWithParameterFilter(new[] { RoleType.Admin, RoleType.Tester, RoleType.Developer })]
        [HttpGet("users")]
        public IActionResult Get([FromHeader] string token)
        {
            IEnumerable<Project> projects = _users.GetProjects(token);
            return Ok(ProjectModel.ToModelList(projects));
        }

        [AuthorizationWithParameterFilter(new[] { RoleType.Admin })]
        [HttpPost]
        public IActionResult Post([FromBody] ProjectAddModel addModel)
        {
            _projects.Add(addModel.ToEntity());
            return Ok();
        }

        [AuthorizationWithParameterFilter(new[] { RoleType.Admin })]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ProjectAddModel addModel)
        {
            _projects.Update(id, addModel.ToEntity());
            return Ok();
        }

        [AuthorizationWithParameterFilter(new[] { RoleType.Admin })]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _projects.Delete(id);
            return Ok();
        }

        [AuthorizationWithParameterFilter(new[] { RoleType.Admin })]
        [HttpPost("{projectId}/users")]
        public IActionResult Post(int projectId, [FromBody] int userId)
        {
            _projects.AssignUserToProject(userId, projectId);
            return Ok();
        }


        [AuthorizationWithParameterFilter(new[] { RoleType.Admin })]
        [HttpDelete("{projectId}/users/{userId}")]
        public IActionResult Delete(int projectId, int userId)
        {
            _projects.DissociateUserFromProject(userId, projectId);
            return Ok();
        }

        [AuthorizationWithParameterFilter(new[] { RoleType.Admin })]
        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<Project> result = _projects.GetAll();
            return Ok(ProjectModel.ToModelList(result));
        }

        [AuthorizationWithParameterFilter(new[] { RoleType.Admin, RoleType.Tester, RoleType.Developer })]
        [HttpGet("{id}")]
        public IActionResult Get([FromHeader] string token, int id)
        {
            Project result = _projects.Get(id, token);
            return Ok(ProjectModel.ToModel(result));
        }
    }
}