using System.Collections.Generic;
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
    public class BugsController : ControllerBase
    {
        private readonly IBugLogic _bugs;

        public BugsController(IBugLogic bugs)
        {
            _bugs = bugs;
        }

        [HttpGet]
        public IActionResult Get([FromBody] UserModel model)
        {
            IEnumerable<Bug> bugs = _bugs.GetAll(model.ToEntity());
            return Ok(bugs);
        }
    }
}
