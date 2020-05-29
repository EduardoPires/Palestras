using MeusFilme.API.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MeusFilme.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) { Configuration = configuration; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AdicionarApiConfig()
                .AdicionarDbContext(Configuration)
                .AdicionarIdentityConfig()
                .AdicionarJwtConfig(Configuration)
                .AdicionarSwaggerConfig();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app
                .UtilizarApiConfig(env)
                .UtilizarSwaggerConfig();
        }
    }
}