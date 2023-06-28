using ColegioMirim.Application.DTO;
using ColegioMirim.WebAPI.Core.Paginator;
using MediatR;

namespace ColegioMirim.Application.Queries.ListarAlunosTurma
{
    public class ListarAlunosTurmaQuery : QueryParams, IRequest<QueryResponse<AlunoTurmaDTO>>
    {
        public string Pesquisa { get; set; }
    }
}
