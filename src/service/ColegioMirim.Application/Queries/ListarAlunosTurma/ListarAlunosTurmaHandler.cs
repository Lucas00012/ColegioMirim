using ColegioMirim.Application.DTO;
using ColegioMirim.Domain.AlunosTurma;
using ColegioMirim.Infrastructure.Data;
using ColegioMirim.WebAPI.Core.Paginator;
using Dapper;
using MediatR;

namespace ColegioMirim.Application.Queries.ListarAlunosTurma
{
    public class ListarAlunosTurmaHandler : IRequestHandler<ListarAlunosTurmaQuery, QueryResponse<AlunoTurmaDTO>>
    {
        private readonly ColegioMirimContext _context;

        public ListarAlunosTurmaHandler(ColegioMirimContext context)
        {
            _context = context;
        }

        public async Task<QueryResponse<AlunoTurmaDTO>> Handle(ListarAlunosTurmaQuery request, CancellationToken cancellationToken)
        {
            var orderByOptions = new List<OrderByOption>
            {
                new(nameof(AlunoTurmaDTO.VinculadoEm), "at", nameof(AlunoTurma.CreatedAt)),
                new(nameof(AlunoTurmaDTO.AlunoId), "at"),
                new(nameof(AlunoTurmaDTO.TurmaId), "at"),
                new(nameof(AlunoTurmaDTO.Ativo), "at"),
                new(nameof(AlunoTurmaDTO.AlunoNome), "a"),
                new(nameof(AlunoTurmaDTO.TurmaNome), "t"),
            };

            var orderBy = orderByOptions
                .FirstOrDefault(c => c.Field == request.OrderBy) ?? orderByOptions.First();

            var count = await _context.Connection.QuerySingleAsync<int>($@"
                SELECT 
                    COUNT(a.Id)
                FROM AlunoTurma AS at
                INNER JOIN Aluno AS a ON a.Id = at.AlunoId
                INNER JOIN Turma AS t ON t.Id = at.TurmaId
                WHERE
                    @Pesquisa IS NULL 
                    OR a.Nome LIKE '%' + @Pesquisa + '%'
                    OR t.Nome LIKE '%' + @Pesquisa + '%'
            ", new { request.Pesquisa });

            var alunos = await _context.Connection.QueryAsync<AlunoTurmaDTO>($@"
                SELECT
                    at.AlunoId,
                    at.TurmaId,
                    at.Ativo,
                    a.Nome AS AlunoNome,
                    t.Nome AS TurmaNome,
                    at.CreatedAt AS VinculadoEm
                FROM AlunoTurma AS at
                INNER JOIN Aluno AS a ON a.Id = at.AlunoId
                INNER JOIN Turma AS t ON t.Id = at.TurmaId
                WHERE
                    @Pesquisa IS NULL 
                    OR a.Nome LIKE '%' + @Pesquisa + '%'
                    OR t.Nome LIKE '%' + @Pesquisa + '%'
                ORDER BY {orderBy.Alias}.{orderBy.Column} {request.Direction}
                OFFSET @Offset ROWS
                FETCH NEXT @PageSize ROWS ONLY
            ", new
            {
                Offset = request.Offset ?? 0,
                PageSize = request.PageSize ?? (Math.Max(count, 1)),
                request.Pesquisa
            });

            return new QueryResponse<AlunoTurmaDTO>
            {
                Count = count,
                Items = alunos.ToList(),
                PageSize = request.PageSize ?? count,
                Page = request.Page ?? 1
            };
        }
    }
}
