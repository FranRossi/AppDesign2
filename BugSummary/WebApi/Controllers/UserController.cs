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
    public class UserController : ControllerBase
    {
        private readonly ILogic<User> _users;

        public UserController(ILogic<User> users)
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
        
        [HttpGet("{Token}")]
        public IActionResult Get(string Token)
        {
            User user = _users.Get(Token);
            if (user == null) {
                return NotFound();
            }
            return Ok();
        }
    }
}
