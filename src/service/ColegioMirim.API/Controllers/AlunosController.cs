using ColegioMirim.Application.Commands.EditarAluno;
using ColegioMirim.Application.Commands.RegistrarAluno;
using ColegioMirim.Application.Queries.ListarAlunos;
using ColegioMirim.Application.Queries.ObterAluno;
using ColegioMirim.WebAPI.Core.Controllers;
using ColegioMirim.WebAPI.Core.Paginator;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ColegioMirim.API.Controllers
{
    [Route("api/alunos")]
    public class AlunosController : MainController 
    {
        private readonly IMediator _mediator;

        public AlunosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ListarAlunos(string pesquisa, string orderBy, OrderDirection? direction, int? page, int? pageSize)
        {
            var query = await _mediator.Send(new ListarAlunosQuery
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
        public async Task<IActionResult> ObterAluno(int id)
        {
            var query = await _mediator.Send(new ObterAlunoQuery
            {
                Id = id
            });

            if (query is null)
                return NotFound();

            return Ok(query);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> EditarAluno(int id, EditarAlunoCommand command)
        {
            command.Id = id;

            var result = await _mediator.Send(command);
            return CustomResponse(result);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> RegistrarAluno(RegistrarAlunoCommand command)
        {
            var result = await _mediator.Send(command);
            return CustomResponse(result);
        }
    }
}
