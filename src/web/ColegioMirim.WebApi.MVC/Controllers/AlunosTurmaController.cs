using ColegioMirim.WebApi.MVC.Models;
using ColegioMirim.WebApi.MVC.Services.Api;
using ColegioMirim.WebAPI.Core.Paginator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ColegioMirim.WebApi.MVC.Controllers
{
    [Authorize(Roles = "admin")]
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

        [HttpGet("/AlunosTurma/Editar/{id}")]
        public async Task<IActionResult> Editar(int id)
        {
            var alunoTurma = await _alunosTurmaService.ObterAlunoTurma(id);

            var model = new EditarAlunoTurmaViewModel
            {
                AlunoId = alunoTurma.AlunoId,
                TurmaId = alunoTurma.TurmaId,
                Ativo = alunoTurma.Ativo
            };

            ViewBag.AlunoTurma = alunoTurma;
            return View(model);
        }

        [HttpPost("/AlunosTurma/Editar/{id}")]
        public async Task<IActionResult> Editar(int id, EditarAlunoTurmaViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var resposta = await _alunosTurmaService.EditarAlunoTurma(id, model);

            if (PossuiErros(resposta))
            {
                TempData["Erros"] = ExtrairErros(ModelState);
                return RedirectToAction("Editar", new { Id = id });
            }

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

        [HttpPost("{id}")]
        public async Task<IActionResult> Remover(int id)
        {
            await _alunosTurmaService.RemoverAlunoTurma(id);
            return RedirectToAction("Index");
        }
    }
}
