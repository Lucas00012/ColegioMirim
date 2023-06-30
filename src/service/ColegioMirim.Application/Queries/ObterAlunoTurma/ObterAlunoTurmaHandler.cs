using ColegioMirim.Application.DTO;
using ColegioMirim.Infrastructure.Data;
using Dapper;
using MediatR;

namespace ColegioMirim.Application.Queries.ObterAlunoTurma
{
    public class ObterAlunoTurmaHandler : IRequestHandler<ObterAlunoTurmaQuery, AlunoTurmaDTO>
    {
        private readonly ColegioMirimContext _context;

        public ObterAlunoTurmaHandler(ColegioMirimContext context)
        {
            _context = context;
        }

        public async Task<AlunoTurmaDTO> Handle(ObterAlunoTurmaQuery request, CancellationToken cancellationToken)
        {
            var dto = await _context.Connection.QuerySingleAsync<AlunoTurmaDTO>(@"
                SELECT 
                    at.Ativo,
                    at.AlunoId,
                    at.TurmaId,
                    a.Nome AS AlunoNome,
                    t.Nome AS TurmaNome,
                    at.CreatedAt AS VinculadoEm
                FROM AlunoTurma AS at
                INNER JOIN Aluno AS a ON a.Id = at.AlunoId
                INNER JOIN Turma AS t ON t.Id = at.TurmaId
                WHERE at.AlunoId = @AlunoId AND at.TurmaId = @TurmaId
            ", new { request.AlunoId, request.TurmaId });

            return dto;
        }
    }
}
