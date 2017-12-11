using Infra.Logs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Site.Extensions;
using Site.Middlewares;

namespace Site
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.Filters.Add(new ServiceFilterAttribute(typeof(ActionLogger)));
            });

            services.AddDependencyInjection();
        }

        public void Configure(IApplicationBuilder app, 
                              IHostingEnvironment env,
                              ILoggerFactory loggerFactory)
        {
            #region Logging

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            #endregion

            #region Environment

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            #endregion

            app.UseStaticFiles();
            app.UseRequestCulture();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{culture=en-US}/{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
