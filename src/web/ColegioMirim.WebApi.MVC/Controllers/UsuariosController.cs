﻿using ColegioMirim.WebApi.MVC.Models;
using ColegioMirim.WebApi.MVC.Services.Api;
using ColegioMirim.WebAPI.Core.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ColegioMirim.WebApi.MVC.Controllers
{
    public class UsuariosController : MainController
    {
        private readonly UsuariosService _usuariosService;
        private readonly IUserSession _userSession;

        public UsuariosController(UsuariosService usuariosService, IUserSession userSession)
        {
            _usuariosService = usuariosService;
            _userSession = userSession;
        }

        [HttpGet]
        [Route("")]
        [Route("login")]
        public IActionResult Login(string returnUrl)
        {
            if (_userSession.IsAuthenticated)
                return RedirecionarPaginaPrincipal(_userSession);

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [Route("")]
        [Route("login")]
        public async Task<IActionResult> Login(UsuarioLoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
                return View(model);

            var resposta = await _usuariosService.Login(model);

            if (PossuiErros(resposta))
                return View(model);

            await _usuariosService.RealizarLogin(resposta);

            if (string.IsNullOrEmpty(returnUrl))
                return RedirecionarPaginaPrincipal(_userSession);

            return LocalRedirect(returnUrl);
        }

        [HttpGet]
        [Authorize]
        public IActionResult AlterarSenha()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AlterarSenha(AlterarSenhaViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var resposta = await _usuariosService.AlterarSenha(model);

            if (PossuiErros(resposta))
                return View(model);

            return RedirecionarPaginaPrincipal(_userSession);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _usuariosService.Logout();
            return RedirectToAction("Login");
        }
    }
}
