using Microsoft.AspNetCore.Http.HttpResults;
using System.Net;

namespace ContactService.API.Middlewares {
    public class GlobalErrorHandlingMiddleware {
        private readonly RequestDelegate _next;

        public GlobalErrorHandlingMiddleware(RequestDelegate next) {
            _next = next;
        }

        public async Task Invoke(HttpContext context) {
            try {
                await _next(context);
            } catch (Exception ex) {
                // Log the exception with a logging framework like Serilog, NLog, etc.

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                await context.Response.WriteAsync(new {
                    StatusCode = context.Response.StatusCode,
                    Message = "Internal Server Error."
                }.ToString());
            }
        }
    }
}
