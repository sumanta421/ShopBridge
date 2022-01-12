using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ShopBridge.Utils
{
    /// <summary>
    /// Custom middleware for logging  exceptions and tracking transaction Ids
    /// </summary>
    public class GlobalMiddleware
    {
        private ILogger<GlobalMiddleware> _logger;
        private RequestDelegate next;
        public GlobalMiddleware(ILogger<GlobalMiddleware> logger, RequestDelegate _next)
        {
             next = _next;
            _logger = logger;
        }
        public async Task Invoke(HttpContext context)
        {
            string xcor = string.Empty;
            try
            {
                if (!context.Request.Headers?.ContainsKey("X-Correlation-Id")??true)
                    xcor=context.Request.Headers["X-Correlation-Id"] = Guid.NewGuid().ToString();
                await next(context);
            }
            catch(ShopBridgeException sbe)
            {
                context.Response.StatusCode = sbe.StatusCode ?? (int)HttpStatusCode.InternalServerError;
                var payload = sbe.GenerateErrorPayload();
                payload.XCorrelationId = xcor;
                _logger.LogError(sbe.InnerException?.Message??sbe.Message);
                await context.Response.WriteAsync(JsonConvert.SerializeObject(payload));
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var payload = new ErrorFormat
                {
                    XCorrelationId = xcor,
                    Message = "Please contact system Admin",
                    ErrorCode = -1
                };
                _logger.LogError(ex.Message, ex.StackTrace);
                await context.Response.WriteAsync(JsonConvert.SerializeObject(payload));

            }
        }

    }
}
