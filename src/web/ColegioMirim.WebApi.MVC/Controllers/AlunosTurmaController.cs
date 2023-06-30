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
    }
}
