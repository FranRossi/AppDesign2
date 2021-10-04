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
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectLogic _projects;

        public ProjectsController(IProjectLogic projects)
        {
            _projects = projects;
        }

        [HttpPost]
        [AuthorizationWithParameterFilter(RoleType.Admin)]
        public IActionResult Post([FromBody] ProjectModel model)
        {
            _projects.Add(model.ToEntity());
            return Ok();
        }

        [HttpPost("{id}")]
        [AuthorizationWithParameterFilter(RoleType.Admin)]
        public IActionResult Post(int id, [FromBody] ProjectModel model)
        {
            _projects.Update(id, model.ToEntity());
            return Ok();
        }

        [HttpPost("{id}")]
        [AuthorizationWithParameterFilter(RoleType.Admin)]
        public IActionResult Delete(int id)
        {
            _projects.Delete(id);
            return Ok();
        }
    }
}
