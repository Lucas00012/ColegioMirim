using AutoMapper;
using ColegioMirim.Application.DTO;
using ColegioMirim.Domain.AlunosTurma;
using ColegioMirim.Domain.Turmas;
using ColegioMirim.Infrastructure.Data;
using ColegioMirim.WebAPI.Core.Identity;
using Dapper;
using MediatR;

namespace ColegioMirim.Application.Queries.ObterTurma
{
    public class ObterTurmaHandler : IRequestHandler<ObterTurmaQuery, TurmaDTO>
    {
        private readonly UserSession _userSession;
        private readonly ColegioMirimContext _context;

        public ObterTurmaHandler(UserSession userSession, ColegioMirimContext context)
        {
            _userSession = userSession;
            _context = context;
        }

        public async Task<TurmaDTO> Handle(ObterTurmaQuery request, CancellationToken cancellationToken)
        {
            var dto = await _context.Connection.QuerySingleAsync<TurmaDTO>(@"
                SELECT
                    t.Id,
                    t.Nome,
                    t.Ativo,
                    t.Ano,
                    t.CreatedAt AS CriadoEm
                FROM Turma AS t
                WHERE t.Id = @Id AND 
                (
                    @IsAdmin = 1 
                    OR EXISTS
                    (
                        SELECT 1 FROM AlunoTurma AS at
                        INNER JOIN Aluno AS a ON a.Id = at.AlunoId
                        WHERE at.TurmaId = t.Id AND a.UsuarioId = @UsuarioId AND at.Ativo = 1 AND t.Ativo = 1
                    )
                )
            ", new { request.Id, _userSession.IsAdmin, _userSession.UsuarioId });

            return dto;
        }
    }
}
