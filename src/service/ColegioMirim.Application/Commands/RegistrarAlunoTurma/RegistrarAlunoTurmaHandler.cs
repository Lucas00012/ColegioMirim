using ColegioMirim.Application.Commands.EditarAlunoTurma;
using ColegioMirim.Application.DTO;
using ColegioMirim.Application.Queries.ObterAlunoTurma;
using ColegioMirim.Core.Messages;
using ColegioMirim.Domain.AlunosTurma;
using MediatR;

namespace ColegioMirim.Application.Commands.RegistrarAlunoTurma
{
    public class RegistrarAlunoTurmaHandler :
        CommandHandler,
        IRequestHandler<RegistrarAlunoTurmaCommand, CommandResponse<AlunoTurmaDTO>>
    {
        private readonly IAlunoTurmaRepository _alunoTurmaRepository;
        private readonly IMediator _mediator;

        public RegistrarAlunoTurmaHandler(IAlunoTurmaRepository alunoTurmaRepository, IMediator mediator)
        {
            _alunoTurmaRepository = alunoTurmaRepository;
            _mediator = mediator;
        }

        public async Task<CommandResponse<AlunoTurmaDTO>> Handle(RegistrarAlunoTurmaCommand request, CancellationToken cancellationToken)
        {
            var alunoTurma = await _alunoTurmaRepository.GetByAlunoIdTurmaId(request.AlunoId, request.TurmaId);
            if (alunoTurma is not null)
            {
                AdicionarErro("Relação já existe");
                return Error<AlunoTurmaDTO>();
            }

            alunoTurma = new AlunoTurma
            {
                AlunoId = request.AlunoId,
                TurmaId = request.TurmaId,
                Ativo = true
            };

            await _alunoTurmaRepository.Create(alunoTurma);

            var dto = await _mediator.Send(new ObterAlunoTurmaQuery
            {
                Id = alunoTurma.Id
            }, cancellationToken);

            return Success(dto);
        }
    }
}
