using EP.DemoLicoes.UI.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.Extensions.DependencyInjection;

namespace EP.DemoLicoes.UI.Configuration
{
    public static class IdentityConfiguration
    {
        public static void ConfigureIdentity(this IServiceCollection services)
        {
            services.AddDefaultIdentity<IdentityUser>()
                .AddDefaultUI(UIFramework.Bootstrap4)
                .AddEntityFrameworkStores<ApplicationDbContext>();
        }
    }
}