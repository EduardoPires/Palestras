using System;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Infra.Logs
{
    public class ActionLogger : IActionFilter
    {
        private readonly ILogger<ActionLogger> _logger;

        public ActionLogger(ILogger<ActionLogger> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            var data = new
            {
                Version = "v1.0",
                User = context.HttpContext.User.Identity.Name,
                IP = context.HttpContext.Connection.RemoteIpAddress.ToString(),
                Hostname = context.HttpContext.Request.Host.ToString(),
                AreaAccessed = context.HttpContext.Request.GetDisplayUrl(),
                Action = context.ActionDescriptor.DisplayName,
                TimeStamp = DateTime.Now
            };

            _logger.LogInformation(1, data.ToString());
            _logger.LogTrace("Passei por aqui");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {

        }
    }
}
