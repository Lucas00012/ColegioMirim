using ColegioMirim.Application.DTO;
using ColegioMirim.Application.Queries.ObterAluno;
using ColegioMirim.Core.Messages;
using ColegioMirim.Domain.Alunos;
using ColegioMirim.Domain.Usuarios;
using ColegioMirim.WebAPI.Core.Identity;
using MediatR;

namespace ColegioMirim.Application.Commands.EditarAlunoPerfil
{
    public class EditarAlunoPerfilHandler :
        CommandHandler,
        IRequestHandler<EditarAlunoPerfilCommand, CommandResponse<AlunoDTO>>
    {
        private readonly UserSession _userSession;
        private readonly IAlunoRepository _alunoRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMediator _mediator;

        public EditarAlunoPerfilHandler(IMediator mediator, UserSession userSession, IAlunoRepository alunoRepository, IUsuarioRepository usuarioRepository)
        {
            _mediator = mediator;
            _userSession = userSession;
            _alunoRepository = alunoRepository;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<CommandResponse<AlunoDTO>> Handle(EditarAlunoPerfilCommand request, CancellationToken cancellationToken)
        {
            if (!request.EhValido())
            {
                AdicionarErros(request.ValidationResult);
                return Error<AlunoDTO>();
            }

            var aluno = await _alunoRepository.GetByUsuarioId(_userSession.UsuarioId.Value);

            var usuario = await _usuarioRepository.GetById(aluno.UsuarioId);

            var usuarioPorEmail = await _usuarioRepository.GetByEmail(request.Email);
            if (usuarioPorEmail is not null && usuarioPorEmail.Id != usuario.Id)
            {
                AdicionarErro("O email já está sendo utilizado por outro usuário");
                return Error<AlunoDTO>();
            }

            usuario.Email = request.Email;
            aluno.Nome = request.Nome;

            await _alunoRepository.Update(aluno);
            await _usuarioRepository.Update(usuario);

            var dto = await _mediator.Send(new ObterAlunoQuery
            {
                Id = aluno.Id
            }, cancellationToken);

            return Success(dto);
        }
    }
}
