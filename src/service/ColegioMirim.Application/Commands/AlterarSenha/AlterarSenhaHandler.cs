using ColegioMirim.Core.Messages;
using ColegioMirim.Domain.Usuarios;
using ColegioMirim.WebAPI.Core.Identity;
using MediatR;

namespace ColegioMirim.Application.Commands.AlterarSenha
{
    public class AlterarSenhaHandler :
        CommandHandler,
        IRequestHandler<AlterarSenhaCommand, CommandResponse>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IUserSession _userSession;

        public AlterarSenhaHandler(IUsuarioRepository usuarioRepository, IUserSession userSession)
        {
            _usuarioRepository = usuarioRepository;
            _userSession = userSession;
        }

        public async Task<CommandResponse> Handle(AlterarSenhaCommand request, CancellationToken cancellationToken)
        {
            if (!request.EhValido())
            {
                AdicionarErros(request.ValidationResult);
                return Error();
            }

            var usuario = await _usuarioRepository.GetById(_userSession.UsuarioId.Value);

            var hashAtual = Usuario.GerarSenhaHash(request.SenhaAtual);
            if (hashAtual != usuario.SenhaHash)
            {
                AdicionarErro("Senha atual não confere");
                return Error();
            }

            usuario.SenhaHash = Usuario.GerarSenhaHash(request.NovaSenha);

            await _usuarioRepository.Update(usuario);

            return Success();
        }
    }
}
