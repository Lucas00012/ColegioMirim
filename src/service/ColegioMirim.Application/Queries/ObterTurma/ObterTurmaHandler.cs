using AutoMapper;
using ColegioMirim.Application.DTO;
using ColegioMirim.Domain.AlunosTurma;
using ColegioMirim.Domain.Turmas;
using ColegioMirim.WebAPI.Core.Identity;
using MediatR;

namespace ColegioMirim.Application.Queries.ObterTurma
{
    public class ObterTurmaHandler : IRequestHandler<ObterTurmaQuery, TurmaDTO>
    {
        private readonly ITurmaRepository _turmaRepository;
        private readonly IAlunoTurmaRepository _alunoTurmaRepository;
        private readonly UserSession _userSession;
        private readonly IMapper _mapper;

        public ObterTurmaHandler(IMapper mapper, UserSession userSession, ITurmaRepository turmaRepository, IAlunoTurmaRepository alunoTurmaRepository)
        {
            _mapper = mapper;
            _userSession = userSession;
            _turmaRepository = turmaRepository;
            _alunoTurmaRepository = alunoTurmaRepository;
        }

        public async Task<TurmaDTO> Handle(ObterTurmaQuery request, CancellationToken cancellationToken)
        {
            var turma = await _turmaRepository.GetById(request.Id);

            if (turma is null)
                return null;

            if (!_userSession.IsAdmin)
            {
                var vinculo = await _alunoTurmaRepository.GetByUsuarioIdTurmaId(_userSession.UsuarioId.Value, request.Id);

                if (vinculo is null || !vinculo.Ativo)
                    return null;
            }

            var dto = _mapper.Map<TurmaDTO>(turma);
            return dto;
        }
    }
}
