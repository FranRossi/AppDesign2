using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLogicInterface;
using Domain.DomainUtilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.Filters
{
    public class AuthorizationWithParameterFilter : Attribute, IAuthorizationFilter
    {
        private static readonly Dictionary<RoleType, string> _messageMap = new()
        {
            { RoleType.Admin, "Admin" },
            { RoleType.Developer, "Developer" },
            { RoleType.Tester, "Tester" }
        };

        private readonly RoleType[] _argument;
        private ISessionLogic _sessionLogic;

        public AuthorizationWithParameterFilter(RoleType[] argument)
        {
            _argument = argument;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!_argument.Contains(RoleType.Invalid))
            {
                _sessionLogic = context.HttpContext.RequestServices.GetService<ISessionLogic>();
                string token = context.HttpContext.Request.Headers["token"];
                RoleType role = _sessionLogic.GetRoleByToken(token);
                if (token == null)
                    context.Result = new ContentResult
                    {
                        StatusCode = 403,
                        Content = "Please send a valid token."
                    };
                else if (!_argument.Contains(role))
                    context.Result = new ContentResult
                    {
                        StatusCode = 401,
                        Content = GetMessageByRole(_argument)
                    };
            }
        }

        private string GetMessageByRole(RoleType[] roles)
        {
            string baseMessage = "Authentication failed: please log in as: \n";
            foreach (var role in roles)
            {
                baseMessage += "- " + _messageMap[role] + "\n";
            }
            return baseMessage;
        }
    }
}