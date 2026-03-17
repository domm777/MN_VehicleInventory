using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using System.Net;


namespace MN_VehicleInventory.Shared.Middleware {
    public class ExceptionMiddleware {
        private readonly RequestDelegate _nextRequest;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate nextRequest, ILogger<ExceptionMiddleware> logger) {
            _nextRequest = nextRequest;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context) {
            try {
                await _nextRequest(context);
            } catch (Exception ex) {
                _logger.LogError(ex, "Something went wrong: {Message}", ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }
        // Helper methods don't need to be public just need it for the 
        // This static Task
        private static Task HandleExceptionAsync(HttpContext context, Exception exception) {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            // Returns new error details here
            var response = new ErrorDetails {
                StatusCode = context.Response.StatusCode,
                Message = "Internal Server Error.",
                Details = exception.Message
            };

            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}