using ColegioMirim.Application.Commands.AlterarSenha;
using ColegioMirim.Application.Commands.RealizarLogin;
using ColegioMirim.Application.Services.JwtToken;
using ColegioMirim.WebAPI.Core.Controllers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ColegioMirim.API.Controllers
{
    [ApiController]
    [Route("api/usuarios")]
    public class UsuariosController : MainController
    {
        private readonly IMediator _mediator;

        public UsuariosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(RealizarLoginCommand command)
        {
            var result = await _mediator.Send(command);
            return CustomResponse(result);
        }

        [HttpPut("alterar-senha")]
        [Authorize]
        public async Task<IActionResult> AlterarSenha(AlterarSenhaCommand command)
        {
            var result = await _mediator.Send(command);
            return CustomResponse(result);
        }
    }
}
