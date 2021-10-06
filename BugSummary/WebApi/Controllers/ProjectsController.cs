using BusinessLogicInterface;
using Domain.DomainUtilities;
using Microsoft.AspNetCore.Mvc;
using System;
using WebApi.Filters;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ExceptionFilter]
    [AuthorizationWithParameterFilter(RoleType.Admin)]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectLogic _projects;

        public ProjectsController(IProjectLogic projects)
        {
            _projects = projects;
        }

        [HttpPost("{id}")]
        public IActionResult Post(int id, [FromBody] ProjectModel model)
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

        [HttpPost("{userId},{projectId}")]
        public IActionResult Post(int userId, int projectId)
        {
            _projects.AssignUserToProject(userId, projectId);
            return Ok();
        }

        [HttpDelete("{userId},{projectId}")]
        public IActionResult Delete(int userId, int projectId)
        {
            _projects.DissociateUserFromProject(userId, projectId);
            return Ok();
        }

        [HttpPost]
        public IActionResult Post([FromBody] string path, [FromQuery] string companyName)
        {
            _projects.AddBugsFromFile(path, companyName);
            return Ok();
        }
    }
}