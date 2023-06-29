using ColegioMirim.WebApi.MVC.Common.Exceptions;
using System.Net;

namespace ColegioMirim.WebApi.MVC.Common.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (CustomHttpRequestException ex)
            {
                HandleRequestExceptionAsync(httpContext, ex.StatusCode);
            }
        }

        private static void HandleRequestExceptionAsync(HttpContext context, HttpStatusCode statusCode)
        {
            if (statusCode == HttpStatusCode.Unauthorized)
                context.Response.Redirect($"/login?ReturnUrl={context.Request.Path}");
            else
                context.Response.StatusCode = (int)statusCode;
        }
    }
}
