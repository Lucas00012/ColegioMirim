using ColegioMirim.WebApi.MVC.Models;
using ColegioMirim.WebApi.MVC.Services.Api;
using ColegioMirim.WebAPI.Core.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ColegioMirim.WebApi.MVC.Controllers
{
    public class UsuariosController : MainController
    {
        private readonly UsuariosService _usuariosService;
        private readonly UserSession _userSession;

        public UsuariosController(UsuariosService usuariosService, UserSession userSession)
        {
            _usuariosService = usuariosService;
            _userSession = userSession;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            if (_userSession.IsAuthenticated)
                return RedirectToAction("Index", "Alunos");

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UsuarioLoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
                return View(model);

            var resposta = await _usuariosService.Login(model);

            if (PossuiErros(resposta))
                return View(model);

            await _usuariosService.RealizarLogin(resposta);

            if (string.IsNullOrEmpty(returnUrl))
                return RedirectToAction("Index", "Alunos");

            return LocalRedirect(returnUrl);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _usuariosService.Logout();
            return RedirectToAction("Login");
        }
    }
}
