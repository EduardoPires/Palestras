using System;
using Elmah.Io.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EP.DemoLicoes.UI.Configuration
{
    public static class MvcConfiguration
    {
        public static void ConfigureMvc(this IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }
    }

    public static class ElmahConfiguration
    {
        public static void ConfigureElmah(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddElmahIo(o =>
            {
                o.ApiKey = configuration.GetValue<string>("ElmahIo:ApiKey");
                o.LogId = new Guid(configuration.GetValue<string>("ElmahIo:LogId"));
            });
        }
    }
}