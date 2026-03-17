using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MN_VehicleInventory.Shared.Middleware {
    public static class ExceptionMiddlewareExtensions {
        public static void ConfigureGlobalExceptionHandler(this IApplicationBuilder app) {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
