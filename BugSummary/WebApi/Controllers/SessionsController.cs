using BusinessLogicInterface;
using Microsoft.AspNetCore.Mvc;
using WebApi.Filters;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SessionsController : ControllerBase
    {
        private readonly ISessionLogic _sessionLogic;

        public SessionsController(ISessionLogic sessionLogic)
        {
            _sessionLogic = sessionLogic;
        }

        [HttpPost]
        [ExceptionFilter]
        public IActionResult Post([FromBody] LoginModel model)
        {
            var token = _sessionLogic.Authenticate(model.Username, model.Password);
            return Ok(token);
        }
    }
}