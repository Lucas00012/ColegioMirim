﻿using ColegioMirim.WebApi.MVC.Models;
using ColegioMirim.WebApi.MVC.Services.Api;
using ColegioMirim.WebAPI.Core.Paginator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ColegioMirim.WebApi.MVC.Controllers
{
    [Authorize]
    public class TurmasController : MainController
    {
        private readonly TurmasService _turmasService;

        public TurmasController(TurmasService turmasService)
        {
            _turmasService = turmasService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string pesquisa, string orderBy, OrderDirection? direction, int? page, int pageSize = 10)
        {
            var turmas = await _turmasService.ListarTurmas(pesquisa, orderBy, direction, page, pageSize);

            return View(turmas);
        }

        [HttpGet("/Turmas/Editar/{id}")]
        public async Task<IActionResult> Editar(int id)
        {
            var turma = await _turmasService.ObterTurma(id);

            var model = new EditarTurmaViewModel
            {
                Ano = turma.Ano,
                Ativo = turma.Ativo,
                Nome = turma.Nome
            };

            ViewBag.TurmaId = turma.Id;
            return View(model);
        }

        [HttpPost("/Turmas/Editar/{id}")]
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
        public IActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
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
