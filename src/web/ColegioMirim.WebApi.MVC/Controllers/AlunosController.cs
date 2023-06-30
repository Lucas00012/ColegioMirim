using ColegioMirim.WebApi.MVC.Models;
using ColegioMirim.WebApi.MVC.Services.Api;
using ColegioMirim.WebAPI.Core.Paginator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ColegioMirim.WebApi.MVC.Controllers
{
    [Authorize]
    public class AlunosController : MainController
    {
        private readonly AlunosService _alunosService;

        public AlunosController(AlunosService alunosService)
        {
            _alunosService = alunosService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string pesquisa, string orderBy, OrderDirection? direction, int? page, int pageSize = 10)
        {
            var alunos = await _alunosService.ListarAlunos(pesquisa, orderBy, direction, page, pageSize);

            return View(alunos);
        }

        [HttpGet("/Alunos/Editar/{id}")]
        public async Task<IActionResult> Editar(int id)
        {
            var aluno = await _alunosService.ObterAluno(id);

            var model = new EditarAlunoViewModel
            {
                Ativo = aluno.Ativo,
                Email = aluno.Email,
                Nome = aluno.Nome,
                RA = aluno.RA
            };

            return View(model);
        }

        [HttpPost("/Alunos/Editar/{id}")]
        public async Task<IActionResult> Editar(int id, EditarAlunoViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var resposta = await _alunosService.EditarAluno(id, model);

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
        public async Task<IActionResult> Registrar(RegistrarAlunoViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var resposta = await _alunosService.RegistrarAluno(model);

            if (PossuiErros(resposta))
                return View(model);

            return RedirectToAction("Index");
        }
    }
}
