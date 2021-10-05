using System;
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
    [ExceptionFilter]
    [Route("[controller]")]
    public class BugsController : ControllerBase
    {
        private readonly IBugLogic _bugs;

        public BugsController(IBugLogic bugs)
        {
            _bugs = bugs;
        }

        [HttpGet]
        [AuthorizationWithParameterFilter(RoleType.Tester)]
        public IActionResult Get([FromHeader] string token)
        {
            IEnumerable<Bug> bugs = _bugs.GetAll(token);
            return Ok(bugs);
        }


        [HttpPut]
        [AuthorizationWithParameterFilter(RoleType.Tester)]
        public IActionResult Put([FromHeader] string token, [FromBody] BugModel bug)
        {
            _bugs.Update(token, bug.ToEntity());
            return Ok();
        }

        [HttpPut("{bugId}")]
        [AuthorizationWithParameterFilter(RoleType.Developer)]
        public IActionResult Put([FromHeader] string token, int bugId)
        {
            _bugs.FixBug(token, bugId);
            return Ok();
        }
    }
}
