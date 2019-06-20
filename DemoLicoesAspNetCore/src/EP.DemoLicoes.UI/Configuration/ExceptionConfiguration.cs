using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace EP.DemoLicoes.UI.Configuration
{
    public static class ExceptionConfiguration
    {
        public static void UseExeptionConfiguration(this IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error/500");
                app.UseStatusCodePagesWithRedirects("/Home/Error/{0}");
            }
        }
    }
}