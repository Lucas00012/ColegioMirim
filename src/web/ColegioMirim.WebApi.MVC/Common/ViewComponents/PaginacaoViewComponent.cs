using ColegioMirim.WebApi.MVC.Models.Response;
using Microsoft.AspNetCore.Mvc;

namespace ColegioMirim.WebApi.MVC.Common.ViewComponents
{
    public class PaginacaoViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(IPaginacao paginacao)
        {
            return View(paginacao);
        }
    }
}
