using EP.DemoLicoes.UI.Extensions;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.Extensions.DependencyInjection;

namespace EP.DemoLicoes.UI.Configuration
{
    public static class DependencyInjectionConfiguration
    {
        public static void ResolveDependencies(this IServiceCollection services)
        {
            services.AddSingleton<IValidationAttributeAdapterProvider, MoedaValidationAttributeAdapterProvider>();
        }
    }
}