using Microsoft.AspNetCore.Http;
using Repository.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace WebApi.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                //await context.Response.WriteAsync($"Error reason \r\n {error.Message}");

                await context.Response.WriteAsync(ConnectionStrings.SqlServer);
            }
        }
    }
}
