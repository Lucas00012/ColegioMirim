using ColegioMirim.WebApi.MVC.Models;
using ColegioMirim.WebApi.MVC.Services.Api;
using ColegioMirim.WebAPI.Core.Paginator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ColegioMirim.WebApi.MVC.Controllers
{
    [Authorize]
    public class AlunosTurmaController : MainController
    {
        private readonly AlunosTurmaService _alunosTurmaService;

        public AlunosTurmaController(AlunosTurmaService alunosTurmaService)
        {
            _alunosTurmaService = alunosTurmaService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string pesquisa, string orderBy, OrderDirection? direction, int? page, int pageSize = 10)
        {
            var alunosTurma = await _alunosTurmaService.ListarAlunosTurma(pesquisa, orderBy, direction, page, pageSize);

            return View(alunosTurma);
        }

        [HttpGet("/AlunosTurma/Editar/{alunoId}/{turmaId}")]
        public async Task<IActionResult> Editar(int alunoId, int turmaId)
        {
            var alunoTurma = await _alunosTurmaService.ObterAlunoTurma(alunoId, turmaId);

            var model = new EditarAlunoTurmaViewModel
            {
                Ativo = alunoTurma.Ativo
            };

            ViewBag.AlunoTurma = alunoTurma;
            return View(model);
        }

        [HttpPost("/AlunosTurma/Editar/{alunoId}/{turmaId}")]
        public async Task<IActionResult> Editar(int alunoId, int turmaId, EditarAlunoTurmaViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var resposta = await _alunosTurmaService.EditarAlunoTurma(alunoId, turmaId, model);

            if (PossuiErros(resposta))
                return View(model);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registrar(RegistrarAlunoTurmaViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var resposta = await _alunosTurmaService.RegistrarAlunoTurma(model);

            if (PossuiErros(resposta))
                return View(model);

            return RedirectToAction("Index");
        }
    }
}
