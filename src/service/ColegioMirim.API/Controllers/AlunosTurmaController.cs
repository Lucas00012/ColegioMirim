using ColegioMirim.Application.Commands.EditarAlunoTurma;
using ColegioMirim.Application.Commands.RegistrarAlunoTurma;
using ColegioMirim.Application.Commands.RemoverAlunoTurma;
using ColegioMirim.Application.Queries.ListarAlunosTurma;
using ColegioMirim.Application.Queries.ObterAlunoTurma;
using ColegioMirim.WebAPI.Core.Controllers;
using ColegioMirim.WebAPI.Core.Identity;
using ColegioMirim.WebAPI.Core.Paginator;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ColegioMirim.API.Controllers
{
    [Route("api/alunos-turma")]
    [Authorize(Roles = "admin")]
    public class AlunosTurmaController : MainController
    {
        private readonly IMediator _mediator;
        private readonly UserSession _userSession;

        public AlunosTurmaController(IMediator mediator, UserSession userSession)
        {
            _mediator = mediator;
            _userSession = userSession;
        }

        [HttpGet]
        public async Task<IActionResult> ListarTurmas(string pesquisa, string orderBy, OrderDirection? direction, int? page, int? pageSize)
        {
            var query = await _mediator.Send(new ListarAlunosTurmaQuery
            {
                Pesquisa = pesquisa,
                Direction = direction,
                OrderBy = orderBy,
                Page = page,
                PageSize = pageSize
            });

            return Ok(query);
        }

        [HttpGet("{alunoId}/{turmaId}")]
        public async Task<IActionResult> ObterAlunoTurma(int alunoId, int turmaId)
        {
            var query = await _mediator.Send(new ObterAlunoTurmaQuery
            {
                AlunoId = alunoId,
                TurmaId = turmaId
            });

            return Ok(query);
        }

        [HttpPut("{alunoId}/{turmaId}")]
        public async Task<IActionResult> EditarAlunoTurma(int alunoId, int turmaId, EditarAlunoTurmaCommand command)
        {
            command.AlunoId = alunoId;
            command.TurmaId = turmaId;
            var result = await _mediator.Send(command);
            return CustomResponse(result);
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarAlunoTurma(RegistrarAlunoTurmaCommand command)
        {
            var result = await _mediator.Send(command);
            return CustomResponse(result);
        }

        [HttpDelete("{alunoId}/{turmaId}")]
        public async Task<IActionResult> RemoverAlunoTurma(int alunoId, int turmaId)
        {
            var result = await _mediator.Send(new RemoverAlunoTurmaCommand
            {
                AlunoId = alunoId,
                TurmaId = turmaId
            });

            return CustomResponse(result);
        }
    }
}
