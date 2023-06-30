using ColegioMirim.Application.Commands.EditarTurma;
using ColegioMirim.Application.Commands.RegistrarTurma;
using ColegioMirim.Application.Queries.ListarTurmas;
using ColegioMirim.Application.Queries.ObterTurma;
using ColegioMirim.WebAPI.Core.Controllers;
using ColegioMirim.WebAPI.Core.Paginator;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ColegioMirim.API.Controllers
{
    [Route("api/turmas")]
    public class TurmasController : MainController
    {
        private readonly IMediator _mediator;

        public TurmasController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ListarTurmas(string pesquisa, string orderBy, OrderDirection? direction, int? page, int? pageSize)
        {
            var query = await _mediator.Send(new ListarTurmasQuery
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
        [Authorize]
        public async Task<IActionResult> ObterTurma(int id)
        {
            var query = await _mediator.Send(new ObterTurmaQuery
            {
                Id = id
            });

            if (query is null)
                return NotFound();

            return Ok(query);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> EditarTurma(int id, EditarTurmaCommand command)
        {
            command.Id = id;

            var result = await _mediator.Send(command);
            return CustomResponse(result);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> RegistrarTurma(RegistrarTurmaCommand command)
        {
            var result = await _mediator.Send(command);
            return CustomResponse(result);
        }
    }
}
