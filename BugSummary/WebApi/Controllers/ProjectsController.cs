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
    [Route("projects")]
    [ExceptionFilter]
    [AuthorizationWithParameterFilter(new[] { RoleType.Admin })]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectLogic _projects;

        public ProjectsController(IProjectLogic projects)
        {
            _projects = projects;
        }

        [HttpPost]
        public IActionResult Post([FromBody] ProjectModel model)
        {
            _projects.Add(model.ToEntity());
            return Ok();
        }


        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ProjectModel model)
        {
            _projects.Update(id, model.ToEntity());
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _projects.Delete(id);
            return Ok();
        }

        [HttpPost("{projectId}/users")]
        public IActionResult Post(int projectId ,[FromBody]int userId)
        {
            _projects.AssignUserToProject(userId, projectId);
            return Ok();
        }

        [HttpDelete("{projectId}/users")]
        public IActionResult Delete(int projectId, [FromBody]int userId)
        {
            _projects.DissociateUserFromProject(userId, projectId);
            return Ok();
        }

        [HttpPost("bugs")]
        public IActionResult Post([FromBody] string path, [FromQuery] string companyName)
        {
            _projects.AddBugsFromFile(path, companyName);
            return Ok();
        }

        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<Project> result = _projects.GetAll();
            return Ok(ProjectBugCountModel.ToModel(result));
        }
    }
}