using DataArc;
using DataArc.OrchestratR;

using EShop.Persistence.Contexts.Identity;
using EShop.Orchestration.Identity.Orchestrators;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace EShop.Modules.Identity.Extensions
{
    public static class IdentityModuleExtensions
    {
        public static IServiceCollection AddIdentityModule(this IServiceCollection services)
        {
            // Register services related to the Identity module here
            var configurationManager = new ConfigurationManager();
            configurationManager
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddConsole());
            ILogger logger = factory.CreateLogger("EShopInProcess");

            services.AddDataArcCore(context =>
            {
                context.AddDbContext<EShopIdentityContext>(options => options.UseSqlServer($"{configurationManager.GetConnectionString("EShopIdentity")}")
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors()
                    .UseLoggerFactory(factory));

            }).AddDataArcOrchestration(
                orch => {
                    orch.AddOrchestrator<GetUserInRoleOrchestrator>();
                    orch.AddOrchestrator<CreateUserInRoleOrchestrator>();
                }
            );

            return services;
        }
    }
}