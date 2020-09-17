using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Bookmark.Middlewares
{
    public class BrandMiddleware
    {
        private readonly RequestDelegate _next;

        public BrandMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            context.Response.Headers["copyright"] = "LamBookmarks2020";
           
            // Call the next delegate/middleware in the pipeline
            await _next(context);
        }
    }
}
