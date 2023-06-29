using AutoMapper;
using ColegioMirim.Application.DTO;
using ColegioMirim.Application.Queries.ObterAluno;
using ColegioMirim.Core.Messages;
using ColegioMirim.Domain.Alunos;
using ColegioMirim.Domain.Usuarios;
using MediatR;

namespace ColegioMirim.Application.Commands.RegistrarAluno
{
    public class RegistrarAlunoHandler : 
        CommandHandler, 
        IRequestHandler<RegistrarAlunoCommand, CommandResponse<AlunoDTO>>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IAlunoRepository _alunoRepository;
        private readonly IMediator _mediator;

        public RegistrarAlunoHandler(IAlunoRepository alunoRepository, IUsuarioRepository usuarioRepository, IMediator mediator)
        {
            _alunoRepository = alunoRepository;
            _usuarioRepository = usuarioRepository;
            _mediator = mediator;
        }

        public async Task<CommandResponse<AlunoDTO>> Handle(RegistrarAlunoCommand request, CancellationToken cancellationToken)
        {
            if (!request.EhValido())
            {
                AdicionarErros(request.ValidationResult);
                return Error<AlunoDTO>();
            }

            var usuario = await _usuarioRepository.GetByEmail(request.Email);
            if (usuario is not null)
            {
                AdicionarErro("O email já está sendo utilizado");
                return Error<AlunoDTO>();
            }

            usuario = new Usuario
            {
                Email = request.Email,
                SenhaHash = Usuario.GerarSenhaHash(request.Senha),
                TipoUsuario = TipoUsuario.Aluno
            };

            await _usuarioRepository.Create(usuario);

            var aluno = await _alunoRepository.GetByRA(request.RA);
            if (aluno is not null)
            {
                AdicionarErro("O RA já está sendo utilizado");
                return Error<AlunoDTO>();
            }

            aluno = new Aluno
            {
                Ativo = true,
                Nome = request.Nome,
                RA = request.RA,
                UsuarioId = usuario.Id
            };

            await _alunoRepository.Create(aluno);

            var dto = await _mediator.Send(new ObterAlunoQuery
            {
                Id = aluno.Id
            }, cancellationToken);

            return Success(dto);
        }
    }
}
