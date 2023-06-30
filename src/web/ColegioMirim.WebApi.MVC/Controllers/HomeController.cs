using Microsoft.AspNetCore.Mvc;

namespace ColegioMirim.WebApi.MVC.Controllers
{
    public class HomeController : MainController
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Route("/Erro/{numero}")]
        public IActionResult Error(int numero)
        {
            return View(numero);
        }
    }
}