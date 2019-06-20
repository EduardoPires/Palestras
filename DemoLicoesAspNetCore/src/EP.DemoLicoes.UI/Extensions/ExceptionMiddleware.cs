using System;
using System.Net;
using System.Threading.Tasks;
using Elmah.Io.AspNetCore;
using Microsoft.AspNetCore.Http;

namespace EP.DemoLicoes.UI.Extensions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            await exception.ShipAsync(context);
            context.Response.Redirect("/Home/Error");
        }
    }
}