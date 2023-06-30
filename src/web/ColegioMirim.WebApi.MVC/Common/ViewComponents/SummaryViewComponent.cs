using ColegioMirim.WebApi.MVC.Models.Response;
using Microsoft.AspNetCore.Mvc;

namespace ColegioMirim.WebApi.MVC.Common.ViewComponents
{
    public class SummaryViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
