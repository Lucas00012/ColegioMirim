using AutoMapper;
using ColegioMirim.Application.DTO;
using ColegioMirim.Core.Messages;
using ColegioMirim.Domain.Alunos;
using ColegioMirim.Domain.Usuarios;
using ColegioMirim.WebAPI.Core.Identity;
using MediatR;
using System.Net;

namespace ColegioMirim.Application.Commands.EditarAluno
{
    public class EditarAlunoHandler :
        CommandHandler,
        IRequestHandler<EditarAlunoCommand, CommandResponse<AlunoDTO>>
    {
        private readonly IAlunoRepository _alunoRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly UserSession _userSession;
        private readonly IMapper _mapper;

        public EditarAlunoHandler(IAlunoRepository alunoRepository, UserSession userSession, IUsuarioRepository usuarioRepository, IMapper mapper)
        {
            _alunoRepository = alunoRepository;
            _userSession = userSession;
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }

        public async Task<CommandResponse<AlunoDTO>> Handle(EditarAlunoCommand request, CancellationToken cancellationToken)
        {
            if (!request.EhValido())
            {
                AdicionarErros(request.ValidationResult);
                return Error<AlunoDTO>();
            }

            var aluno = await _alunoRepository.GetById(request.Id);
            if (aluno is null)
            {
                AdicionarErro("Aluno não encontrado");
                return Error<AlunoDTO>(HttpStatusCode.NotFound);
            }

            if (!_userSession.IsAdmin && aluno.UsuarioId != _userSession.UsuarioId)
            {
                AdicionarErro("Você não tem permissão para editar");
                return Error<AlunoDTO>(HttpStatusCode.Forbidden);
            }

            var usuario = await _usuarioRepository.GetById(aluno.UsuarioId);

            aluno.Nome = request.Nome;
            aluno.RA = request.RA;
            usuario.Email = request.Email;

            if (_userSession.IsAdmin)
                aluno.Ativo = request.Ativo;

            await _alunoRepository.Update(aluno);
            await _usuarioRepository.Update(usuario);

            var dto = _mapper.Map<AlunoDTO>(aluno);
            _mapper.Map(usuario, dto);

            return Success(dto);
        }
    }
}
