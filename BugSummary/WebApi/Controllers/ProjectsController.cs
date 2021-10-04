using BusinessLogicInterface;
using Domain;
using Domain.DomainUtilities;
using Microsoft.AspNetCore.Mvc;
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

        [HttpPost]
        public IActionResult Post([FromBody] ProjectModel model)
        {
            _projects.Add(model.ToEntity());
            return Ok();
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
    }
}
