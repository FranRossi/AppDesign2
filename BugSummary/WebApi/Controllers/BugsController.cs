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
        [AuthorizationWithParameterFilter(new[] { RoleType.Tester, RoleType.Developer, RoleType.Admin })]
        public IActionResult Get([FromHeader] string token, int bugId)
        {
            var bug = _bugs.Get(token, bugId);
            return Ok(BugModel.ToModel(bug));
        }

        [HttpPut("{bugId}")]
        [AuthorizationWithParameterFilter(new[] { RoleType.Tester, RoleType.Admin })]
        public IActionResult Put([FromHeader] string token, [FromBody] BugModel bug, int bugId)
        {
            _bugs.Update(token, bug.ToEntityWithID(bugId));
            return Ok();
        }

        [HttpPost]
        [AuthorizationWithParameterFilter(new[] { RoleType.Tester, RoleType.Admin })]
        public IActionResult Post([FromHeader] string token, [FromBody] BugModel bug)
        {
            _bugs.Add(token, bug.ToEntity());
            return Ok();
        }

        [HttpDelete("{bugId}")]
        [AuthorizationWithParameterFilter(new[] { RoleType.Tester, RoleType.Admin })]
        public IActionResult Delete(int bugId, [FromHeader] string token)
        {
            _bugs.Delete(token, bugId);
            return Ok();
        }

        [HttpPatch("{bugId}")]
        [AuthorizationWithParameterFilter(new[] { RoleType.Developer })]
        public IActionResult Patch([FromHeader] string token, int bugId, int fixingTime)
        {
            _bugs.Fix(token, bugId, fixingTime);
            return Ok();
        }

        [HttpGet]
        [AuthorizationWithParameterFilter(new[] { RoleType.Tester, RoleType.Developer })]
        public IActionResult GetAllFiltered([FromHeader] string token, [FromQuery] BugSearchCriteria criteria)
        {
            var bugs = _bugs.GetAllFiltered(token, criteria);
            return Ok(BugModel.ToModelList(bugs));
        }
    }
}