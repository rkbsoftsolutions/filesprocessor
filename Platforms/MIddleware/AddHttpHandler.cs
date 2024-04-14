using AuthenticationSvc.IdentityClasses;
using AuthenticationSvc.Interface;
using Gateway.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace EsearchSvc.Services.Middelwares
{
    public static class AddHttpHandlerAsync
    {
        public static IHttpClientBuilder AddContextAuthenticationHttpHandler(this IHttpClientBuilder httpClientBuilder)
        {
            httpClientBuilder.Services.AddScoped(typeof(HttpAuthHandler));
            httpClientBuilder.Services.AddScoped(typeof(HttpHandler));
            httpClientBuilder.AddHttpMessageHandler<HttpAuthHandler>();
            httpClientBuilder.AddHttpMessageHandler<HttpHandler>();

            return httpClientBuilder;
        }
    }

    public class HttpHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {

            request.Headers.TryAddWithoutValidation("trackingId", Guid.NewGuid().ToString());
            return base.SendAsync(request, cancellationToken);
        }
    }

    public class HttpAuthHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITokenProcessor _tokenProcessor;
        public HttpAuthHandler(IHttpContextAccessor httpContextAccessor, ITokenProcessor tokenProcessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _tokenProcessor = tokenProcessor;
        }
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            
            _ = request.Headers.TryGetValues("x-access-token",out var value);

            if ((value?.Any() ?? false))
            {
                var token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
                token = token.ToString().Replace("Bearer", string.Empty);
                request.Headers.TryAddWithoutValidation("Authorization", new string[] { $"Bearer {token.ToString().Trim()}" }); 
            }
            else
            {
                var token = _tokenProcessor.GetToken(_httpContextAccessor.UserId(), new List<string> { "Admin" });
                request.Headers.TryAddWithoutValidation("Authorization", new string[] { $"Bearer {token.ToString().Trim()}" });
            }
            return base.SendAsync(request, cancellationToken);
        }
    }
}
