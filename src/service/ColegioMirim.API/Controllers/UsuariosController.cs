using ColegioMirim.API.Services.JwtToken;
using ColegioMirim.API.Services.JwtToken.Models;
using ColegioMirim.WebAPI.Core.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace ColegioMirim.API.Controllers
{
    [ApiController]
    [Route("api/usuarios")]
    public class UsuariosController : MainController
    {
        private readonly JwtTokenService _jwtTokenService;

        public UsuariosController(JwtTokenService jwtTokenService)
        {
            _jwtTokenService = jwtTokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(JwtTokenLogin jwtTokenLogin)
        {
            var token = await _jwtTokenService.GerarJwt(jwtTokenLogin.Email, jwtTokenLogin.Senha);

            if (token is null)
                return ErrorResponse("Credenciais incorretas");

            return Ok(token);
        }
    }
}
