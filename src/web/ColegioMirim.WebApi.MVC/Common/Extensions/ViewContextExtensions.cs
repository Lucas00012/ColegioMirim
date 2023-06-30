using Microsoft.AspNetCore.Mvc.Rendering;

namespace ColegioMirim.WebApi.MVC.Common.Extensions
{
    public static class ViewContextExtensions
    {
        public static string FromRoute(this ViewContext viewContext, string property)
        {
            return viewContext.RouteData.Values[property].ToString();
        }
    }
}
