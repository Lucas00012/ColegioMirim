using ColegioMirim.Core.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ColegioMirim.API.Common.Filters
{
    public class UnitOfWorkFilter : IAsyncActionFilter
    {
        private readonly IUnityOfWork _unityOfWork;

        public UnitOfWorkFilter(IUnityOfWork unityOfWork)
        {
            _unityOfWork = unityOfWork;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.HttpContext.Request.Method == "GET")
            {
                await next();
                return;
            }

            try
            {
                _unityOfWork.BeginTransaction();

                var actionExecuted = await next();

                if (actionExecuted.Exception != null && !actionExecuted.ExceptionHandled
                    || actionExecuted.Result is ObjectResult objectResult && objectResult.StatusCode >= 400
                    || actionExecuted.Result is StatusCodeResult statusCodeResult && statusCodeResult.StatusCode >= 400)
                    _unityOfWork.ClearTransaction();
                else
                    _unityOfWork.CommitTransaction();
            }
            catch (Exception)
            {
                _unityOfWork.ClearTransaction();
            }
        }
    }
}
