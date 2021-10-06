using System;
using System.Collections.Generic;
using BusinessLogicInterface;
using Domain;
using Domain.DomainUtilities;
using Microsoft.AspNetCore.Mvc;
using Utilities.Criterias;
using WebApi.Filters;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [ExceptionFilter]
    [Route("bugs")]
    public class BugsController : ControllerBase
    {
        private readonly IBugLogic _bugs;

        public BugsController(IBugLogic bugs)
        {
            _bugs = bugs;
        }

        [HttpGet("{bugId}")]
        public IActionResult Get([FromHeader] string token, int bugId)
        {
            var bug = _bugs.Get(token, bugId);
            return Ok(BugModel.ToModel(bug));
        }

        [HttpPut]
        [AuthorizationWithParameterFilter(RoleType.Tester)]
        public IActionResult Put([FromHeader] string token, [FromBody] BugModel bug)
        {
            _bugs.Update(token, bug.ToEntity());
            return Ok();
        }

        [HttpPost]
        [AuthorizationWithParameterFilter(RoleType.Tester)]
        public IActionResult Post([FromHeader] string token, [FromBody] BugModel bug)
        {
            _bugs.Add(token, bug.ToEntity());
            return Ok();
        }

        [HttpDelete("{bugId}")]
        [AuthorizationWithParameterFilter(RoleType.Tester)]
        public IActionResult Delete(int bugId, [FromHeader] string token)
        {
            _bugs.Delete(token, bugId);
            return Ok();
        }

        [HttpPut("{bugId}")]
        [AuthorizationWithParameterFilter(RoleType.Developer)]
        public IActionResult Put([FromHeader] string token, int bugId)
        {
            _bugs.FixBug(token, bugId);
            return Ok();
        }

        [HttpGet]
        [AuthorizationWithParameterFilter(RoleType.Tester)]
        public IActionResult GetAllFiltered([FromHeader] string token, [FromQuery] BugSearchCriteria criteria)
        {
            var bugs = _bugs.GetAllFiltered(token, criteria);
            return Ok(bugs);
        }
    }
}