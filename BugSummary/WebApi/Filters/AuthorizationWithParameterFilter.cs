using BusinessLogicInterface;
using Domain.DomainUtilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace WebApi.Filters
{
    public class AuthorizationWithParameterFilter : Attribute, IAuthorizationFilter
    {
        private ISessionLogic _sessionLogic;
        private RoleType _argument;

        public AuthorizationWithParameterFilter(RoleType argument)
        {
            _argument = argument;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            _sessionLogic = context.HttpContext.RequestServices.GetService<ISessionLogic>();
            string token = context.HttpContext.Request.Headers["token"];
            RoleType role = _sessionLogic.GetRoleByToken(token);
            if (token == null)
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 403,
                    Content = "Please send a valid token."
                };
            }
        }
    }
}
