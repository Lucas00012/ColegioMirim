using AutoMapper;
using ColegioMirim.Application.DTO;
using ColegioMirim.Domain.Alunos;
using ColegioMirim.Domain.Usuarios;
using ColegioMirim.WebAPI.Core.Identity;
using MediatR;

namespace ColegioMirim.Application.Queries.ObterAluno
{
    public class ObterAlunoHandler : IRequestHandler<ObterAlunoQuery, AlunoDTO>
    {
        private readonly IAlunoRepository _alunoRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly UserSession _userSession;
        private readonly IMapper _mapper;

        public ObterAlunoHandler(UserSession userSession, IAlunoRepository alunoRepository, IMapper mapper, IUsuarioRepository usuarioRepository)
        {
            _userSession = userSession;
            _alunoRepository = alunoRepository;
            _mapper = mapper;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<AlunoDTO> Handle(ObterAlunoQuery request, CancellationToken cancellationToken)
        {
            var aluno = await _alunoRepository.GetById(request.Id);

            if (aluno is null)
                return null;

            if (!_userSession.IsAdmin && (aluno.UsuarioId != _userSession.UsuarioId || !aluno.Ativo))
                return null;

            var usuario = await _usuarioRepository.GetById(aluno.UsuarioId);

            var dto = _mapper.Map<AlunoDTO>(aluno);
            _mapper.Map(usuario, dto);

            return dto;
        }
    }
}
