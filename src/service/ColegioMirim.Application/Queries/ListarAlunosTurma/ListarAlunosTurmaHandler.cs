using ColegioMirim.Application.DTO;
using ColegioMirim.Domain.Alunos;
using ColegioMirim.Domain.AlunosTurma;
using ColegioMirim.Domain.Turmas;
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
                new(nameof(AlunoTurmaDTO.Id), "at"),
                new(nameof(AlunoTurmaDTO.AlunoId), "at"),
                new(nameof(AlunoTurmaDTO.TurmaId), "at"),
                new(nameof(AlunoTurmaDTO.Ativo), "at"),
                new(nameof(AlunoTurmaDTO.AlunoNome), "a", nameof(Aluno.Nome)),
                new(nameof(AlunoTurmaDTO.AlunoRA), "a", nameof(Aluno.RA)),
                new(nameof(AlunoTurmaDTO.TurmaNome), "t", nameof(Turma.Nome)),
                new(nameof(AlunoTurmaDTO.TurmaAno), "t", nameof(Turma.Ano)),
            };

            var orderBy = orderByOptions
                .FirstOrDefault(c => c.Equals(request.OrderBy)) ?? orderByOptions.First();

            using var connection = _context.BuildConnection();
            var count = await connection.QuerySingleAsync<int>($@"
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

            var alunos = await connection.QueryAsync<AlunoTurmaDTO>($@"
                SELECT
                    at.Id,
                    at.AlunoId,
                    at.TurmaId,
                    at.Ativo,
                    a.Nome AS AlunoNome,
                    a.RA AS AlunoRA,
                    t.Nome AS TurmaNome,
                    t.Ano AS TurmaAno,
                    at.CreatedAt AS VinculadoEm
                FROM AlunoTurma AS at
                INNER JOIN Aluno AS a ON a.Id = at.AlunoId
                INNER JOIN Turma AS t ON t.Id = at.TurmaId
                WHERE
                    @Pesquisa IS NULL 
                    OR a.Nome LIKE '%' + @Pesquisa + '%'
                    OR t.Nome LIKE '%' + @Pesquisa + '%'
                ORDER BY {orderBy.Order} {request.Direction}
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
