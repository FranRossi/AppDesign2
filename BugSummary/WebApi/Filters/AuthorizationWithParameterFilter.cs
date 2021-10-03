using BusinessLogicInterface;
using Domain.DomainUtilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace WebApi.Filters
{
    public class AuthorizationWithParameterFilter : Attribute, IAuthorizationFilter
    {

        private RoleType _argument;

        public AuthorizationWithParameterFilter(RoleType argument)
        {
            _argument = argument;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
        }
    }
}
