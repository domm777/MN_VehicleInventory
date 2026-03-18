//using Microsoft.AspNetCore.Http;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Microsoft.Extensions.Configuration;

//namespace MN_ApiGateWay.Middleware {
//    public class ApiKeyMiddleware {
//        private readonly RequestDelegate _nextRequest;
//        private const string APIKEYNAME = "X-Api-Key";
//        public ApiKeyMiddleware(RequestDelegate next) => _nextRequest = next;

//        public async Task InvokeAsync(HttpContext context, IConfiguration config) {
//            // Read the key from appsetings.json
//            // no need to hardcode

//            var path = context.Request.Path.Value;

//            //if (path.StartsWith("/vehicles-service") || path.StartsWith("/maintenance-service")){
//                var secretToken = config["ApiKey"];
//                if (!context.Request.Headers.TryGetValue(APIKEYNAME, out var extractedApiKey) ||
//                    !extractedApiKey.Equals(secretToken)) {
//                    context.Response.StatusCode = 401;
//                    await context.Response.WriteAsync("Unauthorized: Invalid or missing API Key.");
//                    return;
//                }
//                //if (APIKEYNAME != extractedApiKey) {
//                //    context.Response.StatusCode = 401;
//                //    await context.Response.WriteAsync("Unauthorized client.");
//                //    return;
//                //}

//            //}
//            await _nextRequest(context);
//        }
//    }
//}
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace MN_VehicleInventory.Shared.Middleware {
    public class ApiKeyMiddleware {
        private readonly RequestDelegate _next;

        public ApiKeyMiddleware(RequestDelegate next) => _next = next;

        public async Task InvokeAsync(HttpContext context, IConfiguration config) {
            var validKey = config["ApiKey"];

            if (!context.Request.Headers.TryGetValue("X-Api-Key", out var extractedKey) || extractedKey != validKey) {
                context.Response.StatusCode = 401; // Unauthorized
                await context.Response.WriteAsync("Unauthorized: Invalid or missing API Key.");
                return;
            }

            await _next(context);
        }
    }
}