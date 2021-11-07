using BusinessLogicInterface;
using Domain;
using Domain.DomainUtilities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using WebApi.Filters;
using WebApi.Models;
using ExternalReader;

namespace WebApi.Controllers
{
    [EnableCors("CorsApi")]
    [ApiController]
    [Route("bugReaders")]
    [ExceptionFilter]
    [AuthorizationWithParameterFilter(new[] { RoleType.Admin })]
    public class BugReadersController : ControllerBase
    {
        private readonly IProjectLogic _projects;

        public BugReadersController(IProjectLogic projects)
        {
            _projects = projects;
        }

        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<Tuple<string, IEnumerable<Parameter>>> result = _projects.GetExternalReadersInfo();
            return Ok(BugReaderInfoModel.ToModel(result));
        }

        [HttpPost("bugs")]
        public IActionResult Post([FromBody] BugReaderModel readerModel)
        {
            _projects.AddBugsFromExternalReader(readerModel.Path, readerModel.Parameters);
            return Ok();
        }
    }
}