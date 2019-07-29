using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using DeviousCreation.CqrsStarterTemplate.Web.Infrastructure.MediatrConfiguration;
using DeviousCreation.CqrsStarterTemplate.Web.Infrastructure.ServiceConfiguration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DeviousCreation.CqrsStarterTemplate.Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true);

            builder.AddEnvironmentVariables();

            this.Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services
                .AddConfigurationRoot()
                .AddDataStores(this.Configuration)
                .AddCustomizedMvc();

            var container = new ContainerBuilder();
            container.Populate(services);

            container.RegisterModule(new ApplicationModule());
            container.RegisterModule(new MediatorModule());

            return new AutofacServiceProvider(container.Build());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app
                .AddCustomizedErrorResponse(env)
                .UseStaticFiles()
                .UseStackify(env)
                .UseMvcWithDefaultRoute();
        }
    }
}