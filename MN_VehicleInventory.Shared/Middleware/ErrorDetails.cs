using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MN_VehicleInventory.Shared.Middleware {
    public class ErrorDetails {
        // We don't need to make the status code into a value object
        // because of how we would be outside of the business logic, plus it'll add more complexity.
        // using an integer keeps it simple because you aren't protecting business invariant; just passing an integer that represents an HTTP standard.
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string Details { get; set; }
    }
}
