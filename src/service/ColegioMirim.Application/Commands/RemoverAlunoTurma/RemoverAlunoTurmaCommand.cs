using ColegioMirim.Core.Messages;
using MediatR;

namespace ColegioMirim.Application.Commands.RemoverAlunoTurma
{
    public class RemoverAlunoTurmaCommand : IRequest<CommandResponse>
    {
        public int Id { get; set; }
    }
}
