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
    [Route("sessions")]
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
            string token = _sessionLogic.Authenticate(model.Username, model.Password);
            RoleType role = _sessionLogic.GetRoleByToken(token);
            AuthorizationModel authModel = AuthorizationModel.ToModel(token, role);
            return Ok(authModel);
        }
    }
}