using ColegioMirim.Application.DTO;
using ColegioMirim.Application.Queries.ObterAlunoTurma;
using ColegioMirim.Core.Messages;
using ColegioMirim.Domain.AlunosTurma;
using MediatR;
using System.Net;

namespace ColegioMirim.Application.Commands.EditarAlunoTurma
{
    public class EditarAlunoTurmaHandler :
        CommandHandler,
        IRequestHandler<EditarAlunoTurmaCommand, CommandResponse<AlunoTurmaDTO>>
    {
        private readonly IAlunoTurmaRepository _alunoTurmaRepository;
        private readonly IMediator _mediator;

        public EditarAlunoTurmaHandler(IAlunoTurmaRepository alunoTurmaRepository, IMediator mediator)
        {
            _alunoTurmaRepository = alunoTurmaRepository;
            _mediator = mediator;
        }

        public async Task<CommandResponse<AlunoTurmaDTO>> Handle(EditarAlunoTurmaCommand request, CancellationToken cancellationToken)
        {
            var alunoTurma = await _alunoTurmaRepository.GetByAlunoIdTurmaId(request.AlunoId, request.TurmaId);
            if (alunoTurma is null)
            {
                AdicionarErro("Relação não encontrada");
                return Error<AlunoTurmaDTO>(HttpStatusCode.NotFound);
            }

            alunoTurma.Ativo = request.Ativo;

            await _alunoTurmaRepository.Update(alunoTurma);

            var dto = await _mediator.Send(new ObterAlunoTurmaQuery
            {
                AlunoId = request.AlunoId,
                TurmaId = request.TurmaId
            }, cancellationToken);

            return Success(dto);
        }
    }
}
