using ColegioMirim.Core.Data;
using ColegioMirim.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ColegioMirim.API.Common.Filters
{
    public class UnitOfWorkFilter : IAsyncActionFilter
    {
        private readonly ColegioMirimContext _context;

        public UnitOfWorkFilter(ColegioMirimContext context)
        {
            _context = context;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.HttpContext.Request.Method == "GET")
            {
                await next();
                return;
            }

            var actionExecuted = await next();

            if (actionExecuted.Exception != null && !actionExecuted.ExceptionHandled
                || actionExecuted.Result is ObjectResult objectResult && objectResult.StatusCode >= 400
                || actionExecuted.Result is StatusCodeResult statusCodeResult && statusCodeResult.StatusCode >= 400)
                _context.ClearTransaction();
            else
                _context.CommitTransaction();
        }
    }
}
