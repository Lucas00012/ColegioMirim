using ColegioMirim.Application.DTO;
using ColegioMirim.Application.Queries.ObterAluno;
using ColegioMirim.Domain.Alunos;
using ColegioMirim.WebAPI.Core.Identity;
using MediatR;

namespace ColegioMirim.Application.Queries.ObterAlunoPerfil
{
    public class ObterAlunoPerfilHandler : IRequestHandler<ObterAlunoPerfilQuery, AlunoDTO>
    {
        private readonly IUserSession _userSession;
        private readonly IAlunoRepository _alunoRepository;
        private readonly IMediator _mediator;

        public ObterAlunoPerfilHandler(IUserSession userSession, IMediator mediator, IAlunoRepository alunoRepository)
        {
            _userSession = userSession;
            _mediator = mediator;
            _alunoRepository = alunoRepository;
        }

        public async Task<AlunoDTO> Handle(ObterAlunoPerfilQuery request, CancellationToken cancellationToken)
        {
            var aluno = await _alunoRepository.GetByUsuarioId(_userSession.UsuarioId.Value);

            var dto = await _mediator.Send(new ObterAlunoQuery
            {
                Id = aluno.Id
            }, cancellationToken);

            return dto;
        }
    }
}
