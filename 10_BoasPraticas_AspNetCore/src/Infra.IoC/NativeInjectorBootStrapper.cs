using Infra.Logs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Infra.IoC
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<ILogger<ActionLogger>, Logger<ActionLogger>>();
            services.AddScoped<ActionLogger>();
        }
    }
}
