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
        private static Dictionary<RoleType, string> _messageMap = new Dictionary<RoleType, string>()
        {
            { RoleType.Admin, "Admin" },
            { RoleType.Developer, "Developer" },
            { RoleType.Tester, "Tester" },
        };

        public AuthorizationWithParameterFilter(RoleType argument)
        {
            _argument = argument;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (_argument != RoleType.Invalid)
            {
                _sessionLogic = context.HttpContext.RequestServices.GetService<ISessionLogic>();
                string token = context.HttpContext.Request.Headers["token"];
                RoleType role = _sessionLogic.GetRoleByToken(token);
                if (_argument != RoleType.Admin && token == null  )
                {
                    context.Result = new ContentResult()
                    {
                        StatusCode = 403,
                        Content = "Please send a valid token."
                    };
                }
                else if (_argument != RoleType.Admin  && role != _argument)
                {
                    context.Result = new ContentResult()
                    {
                        StatusCode = 401,
                        Content = GetMessageByRole(_argument)
                    };
                }
            }
        }

        private string GetMessageByRole(RoleType role)
        {
            string baseMessage = "Authentication failed: please log in as ";
            return baseMessage + _messageMap[role];
        }
    }
}
