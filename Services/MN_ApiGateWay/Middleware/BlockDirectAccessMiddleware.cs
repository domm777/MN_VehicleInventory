//namespace MN_ApiGateWay.Middleware {
//    public class BlockDirectAccessMiddleware {
//        private readonly RequestDelegate _next;

//        public BlockDirectAccessMiddleware(RequestDelegate next) {
//            _next = next;
//        }

//        public async Task InvokeAsync(HttpContext context) {
//            // Allow Swagger UI to load directly for testing purposes (Optional, but helpful)
//            if (context.Request.Path.StartsWithSegments("/swagger")) {
//                await _next(context);
//                return;
//            }

//            // Check if the request came from the Gateway
//            if (!context.Request.Headers.TryGetValue("X-From-Gateway", out var fromGateway) || fromGateway != "true") {
//                context.Response.StatusCode = 403; // Forbidden
//                context.Response.ContentType = "application/json";
//                await context.Response.WriteAsync("{\"error\": \"Direct access is blocked. Please use the API Gateway.\"}");
//                return;
//            }

//            // If has header, pass to controller
//            await _next(context);
//        }
//    }

//}

using Microsoft.AspNetCore.Http;

namespace MN_VehicleInventory.Shared.Middleware {
    public class BlockDirectAccessMiddleware {
        private readonly RequestDelegate _next;

        public BlockDirectAccessMiddleware(RequestDelegate next) => _next = next;

        public async Task InvokeAsync(HttpContext context) {
            // Optional: let swagger load for your own testing
            if (context.Request.Path.StartsWithSegments("/swagger")) {
                await _next(context);
                return;
            }

            // Block anyone who doesn't have the Gateway's secret header
            if (!context.Request.Headers.TryGetValue("X-From-Gateway", out var fromGateway) || fromGateway != "true") {
                context.Response.StatusCode = 403; // Forbidden
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync("{\"error\": \"Direct access is blocked. Must use API Gateway.\"}");
                return;
            }

            await _next(context);
        }
    }
}