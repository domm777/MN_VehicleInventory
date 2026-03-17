using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace MN_VehicleInventory.Shared.Middleware {
    public class ApiKeyMiddleware {
        private readonly RequestDelegate _nextRequest;
        private const string APIKEYNAME = "X-Api-Key";
        public ApiKeyMiddleware(RequestDelegate next) => _nextRequest = next;

        public async Task InvokeAsync(HttpContext context, IConfiguration config) {
            // Read the key from appsetings.json
            // no need to hardcode
            var validKey = config["ApiKey"];

            if (!context.Request.Headers.TryGetValue(APIKEYNAME, out var extractedApiKey) || !extractedApiKey.Equals(validKey)) {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Unauthorized: Invalid or missing API Key.");
                return;
            }

            await _nextRequest(context);
        }
    }
}
