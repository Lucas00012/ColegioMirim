using ColegioMirim.WebAPI.Core.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ColegioMirim.WebApi.MVC.Controllers
{
    public class HomeController : MainController
    {
        private readonly IUserSession _userSession;

        public HomeController(IUserSession userSession)
        {
            _userSession = userSession;
        }

        public IActionResult Index()
        {
            return RedirecionarPaginaPrincipal(_userSession);
        }

        [Route("/acesso-negado")]
        public IActionResult Forbidden()
        {
            return RedirectToAction("Error", new { numero = 403 });
        }

        [Route("/Erro/{numero}")]
        public IActionResult Error(int numero)
        {
            return View(numero);
        }
    }
}