using BusinessLogicInterface;
using Domain;
using Domain.DomainUtilities;
using Microsoft.AspNetCore.Mvc;
using System;
using WebApi.Filters;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [AuthorizationWithParameterFilter(new[] { RoleType.Admin })]
    [ExceptionFilter]
    public class UsersController : ControllerBase
    {
        private readonly IUserLogic _users;

        public UsersController(IUserLogic users)
        {
            _users = users;
        }

        [HttpPost]
        public IActionResult Post([FromBody] UserModel model)
        {
            _users.Add(model.ToEntity());
            return Ok();
        }

        [HttpGet]
        public IActionResult Get([FromQuery] int id)
        {
            int result = _users.GetFixedBugCount(id);
            return Ok(result);
        }
    }
}