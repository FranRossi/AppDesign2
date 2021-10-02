using BusinessLogic;
using BusinessLogicInterface;
using DataAccess;
using DataAccessInterface;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factory.Factories
{
    public class ServiceFactory
    {
        private readonly IServiceCollection services;

        public ServiceFactory(IServiceCollection services)
        {
            this.services = services;
        }

        public void AddCustomServices()
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRepository<User>, UserRepository>();
            services.AddScoped<ILogic<User>, UserLogic>();
            services.AddScoped<ISessionLogic, SessionLogic>();
        }

        public void AddDbContextService(string connectionString)
        {
            services.AddDbContext<BugSummaryContext>(options => options.UseSqlServer(connectionString));
        }
    }
}
