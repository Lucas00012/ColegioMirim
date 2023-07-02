using ColegioMirim.Application.Services.JwtToken;
using ColegioMirim.Application.Services.JwtToken.Models;
using ColegioMirim.Core.Messages;
using ColegioMirim.Domain.Alunos;
using ColegioMirim.Domain.Usuarios;
using MediatR;

namespace ColegioMirim.Application.Commands.RealizarLogin
{
    public class RealizarLoginHandler :
        CommandHandler,
        IRequestHandler<RealizarLoginCommand, CommandResponse<JwtTokenResult>>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IAlunoRepository _alunoRepository;
        private readonly JwtTokenService _jwtTokenService;

        public RealizarLoginHandler(IUsuarioRepository usuarioRepository, JwtTokenService jwtTokenService, IAlunoRepository alunoRepository)
        {
            _usuarioRepository = usuarioRepository;
            _jwtTokenService = jwtTokenService;
            _alunoRepository = alunoRepository;
        }

        public async Task<CommandResponse<JwtTokenResult>> Handle(RealizarLoginCommand request, CancellationToken cancellationToken)
        {
            if (!request.EhValido())
            {
                AdicionarErros(request.ValidationResult);
                return Error<JwtTokenResult>();
            }

            var usuario = await _usuarioRepository.GetByEmail(request.Email);
            if (usuario is null)
            {
                AdicionarErro("Credenciais incorretas");
                return Error<JwtTokenResult>();
            }

            if (usuario.SenhaHash != Usuario.GerarSenhaHash(request.Senha))
            {
                AdicionarErro("Credenciais incorretas");
                return Error<JwtTokenResult>();
            }

            var aluno = await _alunoRepository.GetByUsuarioId(usuario.Id);
            if (aluno is not null && !aluno.Ativo)
            {
                AdicionarErro("Conta inativa");
                return Error<JwtTokenResult>();
            }

            var token = await _jwtTokenService.GerarJwt(usuario);
            return Success(token);
        }
    }
}
