using ColegioMirim.Application.DTO;
using MediatR;

namespace ColegioMirim.Application.Queries.ObterAlunoTurma
{
    public class ObterAlunoTurmaQuery : IRequest<AlunoTurmaDTO>
    {
        public int Id { get; set; }
    }
}
