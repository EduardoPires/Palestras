using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Core.Interceptors;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using WebMvc.Extensions;

namespace WebMvc.GrpcServices
{
    public class CustomCredentialsInterceptor : Interceptor
    {
        private readonly ILogger<CustomCredentialsInterceptor> _logger;
        private readonly IHttpContextAccessor _httpContextAccesor;

        public CustomCredentialsInterceptor(
            ILogger<CustomCredentialsInterceptor> logger, 
            IHttpContextAccessor httpContextAccesor)
        {
            _logger = logger;
            _httpContextAccesor = httpContextAccesor;
        }

        public override AsyncUnaryCall<TResponse> AsyncUnaryCall<TRequest, TResponse>(
            TRequest request,
            ClientInterceptorContext<TRequest, TResponse> context,
            AsyncUnaryCallContinuation<TRequest, TResponse> continuation)
        {
            var token = _httpContextAccesor.HttpContext.User.Claims
                .FirstOrDefault(c => c.Type == "JWT")?.Value;

            var headers = new Metadata
            {
                { "Authorization", $"Bearer {token}" }
            };

            var options = context.Options.WithHeaders(headers);
            context = new ClientInterceptorContext<TRequest, TResponse>(context.Method, context.Host, options);
            
            var call = continuation(request, context);

            return new AsyncUnaryCall<TResponse>(HandleResponse(call.ResponseAsync), call.ResponseHeadersAsync, call.GetStatus, call.GetTrailers, call.Dispose);
        }

        private async Task<TResponse> HandleResponse<TResponse>(Task<TResponse> t)
        {
            try
            {
                var response = await t;
                _logger.LogDebug($"Response received: {response}");
                return response;
            }
            catch (RpcException ex)
            {
                if (ex.StatusCode == StatusCode.Unauthenticated)
                    throw new CustomHttpRequestException(HttpStatusCode.Unauthorized);

                _logger.LogError($"Call error: {ex.Message}");
                throw;
            }
        }

    }
}