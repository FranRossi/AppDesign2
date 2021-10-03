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
    public class UsersController : ControllerBase
    {
        private readonly ILogic<User> _users;

        public UsersController(ILogic<User> users)
        {
            _users = users;
        }

        [HttpPost]
        [AuthorizationWithParameterFilter(RoleType.Admin)]
        public IActionResult Post([FromBody] UserModel model)
        {
            _users.Add(model.ToEntity());
            return Ok();
        }
    }
}
