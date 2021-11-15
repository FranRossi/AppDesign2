using BusinessLogicInterface;
using Domain;
using Domain.DomainUtilities;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using WebApi.Filters;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("users")]
    [EnableCors("CorsApi")]
    [ExceptionFilter]
    [AuthorizationWithParameterFilter(new[] { RoleType.Admin })]
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
        public IActionResult Get()
        {
            IEnumerable<User> users = _users.GetAll();
            return Ok(UserModel.ToModelList(users));
        }
    }
}