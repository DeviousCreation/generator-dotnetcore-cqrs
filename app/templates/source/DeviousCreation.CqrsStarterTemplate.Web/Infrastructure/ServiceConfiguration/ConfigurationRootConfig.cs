using DeviousCreation.CqrsStarterTemplate.Queries.ConnectionProviders;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DeviousCreation.CqrsStarterTemplate.Web.Infrastructure.ServiceConfiguration
{
    public static class ConfigurationRootConfig
    {
        public static IServiceCollection AddConfigurationRoot(
            this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IDbConnectionProvider, SqlConnectionProvider>();
            return services;
        }
    }
}