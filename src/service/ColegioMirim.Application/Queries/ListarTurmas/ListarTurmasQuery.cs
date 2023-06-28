using ColegioMirim.Application.DTO;
using ColegioMirim.WebAPI.Core.Paginator;
using MediatR;

namespace ColegioMirim.Application.Queries.ListarTurmas
{
    public class ListarTurmasQuery : QueryParams, IRequest<QueryResponse<TurmaDTO>>
    {
        public string Pesquisa { get; set; }
    }
}
