using ColegioMirim.Application.DTO;
using ColegioMirim.Domain.Alunos;
using ColegioMirim.Infrastructure.Data;
using ColegioMirim.WebAPI.Core.Identity;
using ColegioMirim.WebAPI.Core.Paginator;
using Dapper;
using MediatR;

namespace ColegioMirim.Application.Queries.ListarAlunos
{
    public class ListarAlunosHandler : IRequestHandler<ListarAlunosQuery, QueryResponse<AlunoDTO>>
    {
        private readonly ColegioMirimContext _context;
        private readonly UserSession _userSession;

        public ListarAlunosHandler(ColegioMirimContext context, UserSession userSession)
        {
            _context = context;
            _userSession = userSession;
        }

        public async Task<QueryResponse<AlunoDTO>> Handle(ListarAlunosQuery request, CancellationToken cancellationToken)
        {
            var orderByOptions = new List<OrderByOption>
            {
                new(nameof(AlunoDTO.Id), "a"),
                new(nameof(AlunoDTO.RA), "a"),
                new(nameof(AlunoDTO.Nome), "a"),
                new(nameof(AlunoDTO.Email), "u"),
                new(nameof(AlunoDTO.Ativo), "u"),
                new(nameof(AlunoDTO.CriadoEm), "a", nameof(Aluno.CreatedAt)),
            };

            var orderBy = orderByOptions
                .FirstOrDefault(c => c.Field == request.OrderBy) ?? orderByOptions.First();

            var count = await _context.Connection.QuerySingleAsync<int>($@"
                SELECT 
                    COUNT(a.Id)
                FROM Aluno AS a
                INNER JOIN Usuario AS u ON u.Id = a.UsuarioId
                WHERE 
                    (
                        @Pesquisa IS NULL 
                        OR a.Nome LIKE '%' + @Pesquisa + '%'
                        OR u.Email LIKE '%' + @Pesquisa + '%'
                        OR a.RA LIKE '%' + @Pesquisa + '%'
                    )
                    AND 
                    (
                        @IsAdmin = 1 
                        OR a.UsuarioId = @UsuarioId AND a.Ativo = 1
                    )
            ", new
            {
                request.Pesquisa,
                _userSession.UsuarioId,
                _userSession.IsAdmin
            });

            var alunos = await _context.Connection.QueryAsync<AlunoDTO>($@"
                SELECT 
                    a.Id,
                    a.RA,
                    a.Nome,
                    u.Email,
                    a.Ativo,
                    a.CreatedAt as CriadoEm
                FROM Aluno AS a
                INNER JOIN Usuario AS u ON u.Id = a.UsuarioId
                WHERE 
                    (
                        @Pesquisa IS NULL 
                        OR a.Nome LIKE '%' + @Pesquisa + '%'
                        OR u.Email LIKE '%' + @Pesquisa + '%'
                        OR a.RA LIKE '%' + @Pesquisa + '%'
                    )
                    AND 
                    (
                        @IsAdmin = 1 
                        OR a.UsuarioId = @UsuarioId AND a.Ativo = 1
                    )
                ORDER BY {orderBy.Alias}.{orderBy.Column} {request.Direction}
                OFFSET @Offset ROWS
                FETCH NEXT @PageSize ROWS ONLY
            ", new
            {
                Offset = request.Offset ?? 0,
                PageSize = request.PageSize ?? (Math.Max(count, 1)),
                request.Pesquisa,
                _userSession.UsuarioId,
                _userSession.IsAdmin
            });

            return new QueryResponse<AlunoDTO>
            {
                Count = count,
                PageSize = request.PageSize ?? count,
                Items = alunos.ToList(),
                Page = request.Page ?? 1
            };
        }
    }
}
