using ColegioMirim.Application.DTO;
using ColegioMirim.Core.Messages;
using MediatR;
using System.Text.Json.Serialization;

namespace ColegioMirim.Application.Commands.EditarAlunoTurma
{
    public class EditarAlunoTurmaCommand : IRequest<CommandResponse<AlunoTurmaDTO>>
    {
        public bool Ativo { get; set; }

        [JsonIgnore]
        public int AlunoId { get; set; }

        [JsonIgnore]
        public int TurmaId { get; set; }
    }
}
