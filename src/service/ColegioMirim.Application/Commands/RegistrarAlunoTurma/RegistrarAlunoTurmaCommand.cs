using ColegioMirim.Application.DTO;
using ColegioMirim.Core.Messages;
using MediatR;

namespace ColegioMirim.Application.Commands.RegistrarAlunoTurma
{
    public class RegistrarAlunoTurmaCommand : IRequest<CommandResponse<AlunoTurmaDTO>>
    {
        public int AlunoId { get; set; }
        public int TurmaId { get; set; }
    }
}
