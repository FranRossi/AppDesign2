using BusinessLogicInterface;
using Domain;
using Domain.DomainUtilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        
        [HttpGet("{token}")]
        public IActionResult Get(string token)
        {
            User user = _users.Get(token);
            if (user == null) {
                return NotFound();
            }
            return Ok();
        }
    }
}
