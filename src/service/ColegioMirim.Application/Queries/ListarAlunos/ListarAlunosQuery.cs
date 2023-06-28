using ColegioMirim.Application.DTO;
using ColegioMirim.WebAPI.Core.Paginator;
using MediatR;

namespace ColegioMirim.Application.Queries.ListarAlunos
{
    public class ListarAlunosQuery : QueryParams, IRequest<QueryResponse<AlunoDTO>>
    {
        public string Pesquisa { get; set; }
    }
}
