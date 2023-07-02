using ColegioMirim.WebApi.MVC.Models;
using ColegioMirim.WebApi.MVC.Services.Api;
using ColegioMirim.WebAPI.Core.Identity;
using ColegioMirim.WebAPI.Core.Paginator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ColegioMirim.WebApi.MVC.Controllers
{
    public class TurmasController : MainController
    {
        private readonly TurmasService _turmasService;
        private readonly IUserSession _userSession;

        public TurmasController(TurmasService turmasService, IUserSession userSession)
        {
            _turmasService = turmasService;
            _userSession = userSession;
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Index(string pesquisa, string orderBy, OrderDirection? direction, int? page, int pageSize = 10)
        {
            var turmas = await _turmasService.ListarTurmas(pesquisa, orderBy, direction, page, pageSize);

            return View(turmas);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Matricula(string pesquisa, string orderBy, OrderDirection? direction, int? page, int pageSize = 10)
        {
            if (_userSession.IsAdmin)
                return RedirectToAction("Index");

            var turmas = await _turmasService.ListarTurmas(pesquisa, orderBy, direction, page, pageSize);

            return View(turmas);
        }

        [HttpGet("/Turmas/Editar/{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Editar(int id)
        {
            var turma = await _turmasService.ObterTurma(id);

            var model = new EditarTurmaViewModel
            {
                Ano = turma.Ano,
                Ativo = turma.Ativo,
                Nome = turma.Nome
            };

            return View(model);
        }

        [HttpPost("/Turmas/Editar/{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Editar(int id, EditarTurmaViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var resposta = await _turmasService.EditarTurma(id, model);

            if (PossuiErros(resposta))
                return View(model);

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Registrar(RegistrarTurmaViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var resposta = await _turmasService.RegistrarTurma(model);

            if (PossuiErros(resposta))
                return View(model);

            return RedirectToAction("Index");
        }
    }
}
