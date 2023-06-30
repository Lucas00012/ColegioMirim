using ColegioMirim.Application.DTO;
using ColegioMirim.Core.Messages;
using MediatR;
using System.Text.Json.Serialization;

namespace ColegioMirim.Application.Commands.EditarAlunoTurma
{
    public class EditarAlunoTurmaCommand : IRequest<CommandResponse<AlunoTurmaDTO>>
    {
        [JsonIgnore]
        public int Id { get; set; }

        public bool Ativo { get; set; }
        public int AlunoId { get; set; }
        public int TurmaId { get; set; }
    }
}
