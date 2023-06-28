using ColegioMirim.Core.Messages;
using ColegioMirim.Domain.AlunosTurma;
using MediatR;
using System.Net;

namespace ColegioMirim.Application.Commands.RemoverAlunoTurma
{
    public class RemoverAlunoTurmaHandler : CommandHandler, IRequestHandler<RemoverAlunoTurmaCommand, CommandResponse>
    {
        private readonly IAlunoTurmaRepository _alunoTurmaRepository;

        public RemoverAlunoTurmaHandler(IAlunoTurmaRepository alunoTurmaRepository)
        {
            _alunoTurmaRepository = alunoTurmaRepository;
        }

        public async Task<CommandResponse> Handle(RemoverAlunoTurmaCommand request, CancellationToken cancellationToken)
        {
            var alunoTurma = await _alunoTurmaRepository.GetByAlunoIdTurmaId(request.AlunoId, request.TurmaId);
            if (alunoTurma is null)
            {
                AdicionarErro("Relação não encontrada");
                return Error(HttpStatusCode.NotFound);
            }

            await _alunoTurmaRepository.Delete(alunoTurma);

            return Success();
        }
    }
}
