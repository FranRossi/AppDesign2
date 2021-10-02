using BusinessLogicInterface;
using Domain;
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
    public class SessionController : ControllerBase
    {
        private readonly ISessionLogic _sessionLogic;

        public SessionController(ISessionLogic sessionLogic)
        {
            _sessionLogic = sessionLogic;
        }

        [HttpPost]
        [ExceptionFilter]
        public IActionResult Post([FromBody] LoginModel model)
        {
            string token = _sessionLogic.Authenticate(model.Username, model.Password);
            return Ok(token);
        }
    }
}
