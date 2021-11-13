using BusinessLogicInterface;
using Domain.DomainUtilities;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WebApi.Filters;
using WebApi.Models;

namespace WebApi.Controllers
{
    [EnableCors("CorsApi")]
    [ApiController]
    [ExceptionFilter]
    [Route("assignments")]
    public class AssignmentController : ControllerBase
    {
        private readonly IAssignmentLogic _assignments;

        public AssignmentController(IAssignmentLogic assignments)
        {
            _assignments = assignments;
        }

        [HttpPost]
        [AuthorizationWithParameterFilter(new[] { RoleType.Admin })]
        public IActionResult Post([FromBody] AssignmentModel assignment)
        {
            _assignments.Add(assignment.ToEntity());
            return Ok();
        }

    }
}