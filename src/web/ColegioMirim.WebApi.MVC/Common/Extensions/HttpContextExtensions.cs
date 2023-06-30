namespace ColegioMirim.WebApi.MVC.Common.Extensions
{
    public static class HttpContextExtensions
    {
        public static string FromQuery(this HttpContext httpContext, string property)
        {
            return httpContext.Request.Query[property].ToString();
        }
    }
}
