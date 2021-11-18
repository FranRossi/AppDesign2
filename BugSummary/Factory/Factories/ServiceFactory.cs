using BusinessLogic;
using BusinessLogicInterface;
using DataAccess;
using DataAccessInterface;
using Domain;
using BugReaderImporter;
using ExternalReaderImporterInterface;
using FileHandlerFactory;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

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
            services.AddScoped<IUserLogic, UserLogic>();
            services.AddScoped<ISessionLogic, SessionLogic>();
            services.AddScoped<IProjectLogic, ProjectLogic>();
            services.AddScoped<IBugLogic, BugLogic>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IBugRepository, BugRepository>();
            services.AddScoped<IAssignmentRepository, AssignmentRepository>();
            services.AddScoped<IAssignmentLogic, AssignmentLogic>();

        }

        public void AddDbContextService(string connectionString)
        {
            services.AddDbContext<BugSummaryContext>(options => options.UseSqlServer(connectionString));
        }

        public void AddDbExternalReaderService(string pathToFolder)
        {
            services.AddSingleton<IBugReaderImporter>(new BugReaderImporterLogic(pathToFolder));
        }
    }
}