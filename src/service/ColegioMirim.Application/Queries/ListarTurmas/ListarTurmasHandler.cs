using ColegioMirim.Application.DTO;
using ColegioMirim.Infrastructure.Data;
using ColegioMirim.WebAPI.Core.Identity;
using ColegioMirim.WebAPI.Core.Paginator;
using Dapper;
using MediatR;
using Microsoft.IdentityModel.Tokens;

namespace ColegioMirim.Application.Queries.ListarTurmas
{
    public class ListarTurmasHandler : IRequestHandler<ListarTurmasQuery, QueryResponse<TurmaDTO>>
    {
        private readonly ColegioMirimContext _context;
        private readonly UserSession _userSession;

        public ListarTurmasHandler(ColegioMirimContext context, UserSession userSession)
        {
            _context = context;
            _userSession = userSession;
        }

        public async Task<QueryResponse<TurmaDTO>> Handle(ListarTurmasQuery request, CancellationToken cancellationToken)
        {
            var orderByOptions = new List<OrderByOption>
            {
                new(nameof(TurmaDTO.Id), "t"),
                new(nameof(TurmaDTO.Nome), "t"),
                new(nameof(TurmaDTO.Ano), "t"),
                new(nameof(TurmaDTO.Ativo), "t"),
            };

            var orderBy = orderByOptions
                .FirstOrDefault(c => c.Field == request.OrderBy) ?? orderByOptions.First();

            var count = await _context.Connection.QuerySingleAsync<int>($@"
                SELECT 
                    COUNT(t.Id)
                FROM Turma AS t
                WHERE 
                    (
                        @Pesquisa IS NULL 
                        OR t.Nome LIKE '%' + @Pesquisa + '%'
                        OR t.Ano LIKE '%' + @Pesquisa + '%'
                    )
                    AND 
                    (
                        @IsAdmin = 1 
                        OR EXISTS 
                        (
                            SELECT 1 FROM AlunoTurma AS at 
                            INNER JOIN Aluno AS a ON a.Id = at.AlunoId
                            WHERE at.TurmaId = t.Id AND a.UsuarioId = @UsuarioId AND at.Ativo = 1
                        )
                    )
            ", new
            {
                request.Pesquisa,
                _userSession.UsuarioId,
                _userSession.IsAdmin
            });

            var turmas = await _context.Connection.QueryAsync<TurmaDTO>($@"
                SELECT
                    t.Id,
                    t.Nome,
                    t.Ano,
                    t.Ativo,
                    t.CreatedAt as CriadoEm
                FROM Turma AS t
                WHERE 
                    (
                        @Pesquisa IS NULL 
                        OR t.Nome LIKE '%' + @Pesquisa + '%'
                        OR t.Ano LIKE '%' + @Pesquisa + '%'
                    )
                    AND 
                    (
                        @IsAdmin = 1 
                        OR EXISTS 
                        (
                            SELECT 1 FROM AlunoTurma AS at 
                            INNER JOIN Aluno AS a ON a.Id = at.AlunoId
                            WHERE at.TurmaId = t.Id AND a.UsuarioId = @UsuarioId AND at.Ativo = 1
                        )
                    )
                ORDER BY {orderBy.Alias}.{orderBy.Column} {request.Direction}
                OFFSET @Offset ROWS
                FETCH NEXT @PageSize ROWS ONLY
            ", new
            {
                Offset = request.Offset ?? 0,
                PageSize = request.PageSize ?? (Math.Max(count, 1)),
                request.Pesquisa,
                _userSession.IsAdmin,
                _userSession.UsuarioId
            });

            return new QueryResponse<TurmaDTO>
            {
                Count = count,
                PageSize = request.PageSize ?? count,
                Items = turmas.ToList(),
                Page = request.Page ?? 1
            };
        }
    }
}
