using ColegioMirim.WebApi.MVC.Common.Exceptions;
using ColegioMirim.WebApi.MVC.Services.Api;
using System.Net;

namespace ColegioMirim.WebApi.MVC.Common.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private static UsuariosService _usuariosService;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext, UsuariosService usuariosService)
        {
            _usuariosService = usuariosService;

            try
            {
                await _next(httpContext);
            }
            catch (CustomHttpRequestException ex)
            {
                await HandleRequestExceptionAsync(httpContext, ex.StatusCode);
            }
        }

        private static async Task HandleRequestExceptionAsync(HttpContext context, HttpStatusCode statusCode)
        {
            if (statusCode == HttpStatusCode.Unauthorized)
            {
                await _usuariosService.Logout();
                context.Response.Redirect($"/login?ReturnUrl={context.Request.Path}");
            }
            else
                context.Response.StatusCode = (int)statusCode;
        }
    }
}
