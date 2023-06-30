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

        public AlunosTurmaController(IMediator mediator)
        {
            _mediator = mediator;
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

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterAlunoTurma(int id)
        {
            var query = await _mediator.Send(new ObterAlunoTurmaQuery
            {
                Id = id
            });

            if (query is null)
                return NotFound();

            return Ok(query);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditarAlunoTurma(int id, EditarAlunoTurmaCommand command)
        {
            command.Id = id;

            var result = await _mediator.Send(command);
            return CustomResponse(result);
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarAlunoTurma(RegistrarAlunoTurmaCommand command)
        {
            var result = await _mediator.Send(command);
            return CustomResponse(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoverAlunoTurma(int id)
        {
            var result = await _mediator.Send(new RemoverAlunoTurmaCommand
            {
                Id = id
            });

            return CustomResponse(result);
        }
    }
}
