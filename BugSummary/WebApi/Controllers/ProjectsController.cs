using BusinessLogicInterface;
using Domain;
using Domain.DomainUtilities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using WebApi.Filters;
using WebApi.Models;

namespace WebApi.Controllers
{
    [EnableCors("CorsApi")]
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
        public IActionResult Post([FromBody] ProjectAddModel addModel)
        {
            _projects.Add(addModel.ToEntity());
            return Ok();
        }


        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ProjectAddModel addModel)
        {
            _projects.Update(id, addModel.ToEntity());
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _projects.Delete(id);
            return Ok();
        }

        [HttpPost("{projectId}/users")]
        public IActionResult Post(int projectId, [FromBody] int userId)
        {
            _projects.AssignUserToProject(userId, projectId);
            return Ok();
        }

        [HttpDelete("{projectId}/users")]
        public IActionResult Delete(int projectId, [FromBody] int userId)
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
            return Ok(ProjectModel.ToModel(result));
        }
    }
}