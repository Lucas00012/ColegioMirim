using ColegioMirim.Domain.AlunosTurma;
using Dapper;

namespace ColegioMirim.Infrastructure.Data.Repository
{
    public class AlunoTurmaRepository : IAlunoTurmaRepository
    {
        private readonly ColegioMirimContext _context;

        public AlunoTurmaRepository(ColegioMirimContext context)
        {
            _context = context;
        }

        public async Task<int> Count()
        {
            var sql = "SELECT COUNT(*) FROM AlunoTurma";
            var count = await _context.Connection.QuerySingleAsync<int>(sql);

            return count;
        }

        public async Task<AlunoTurma> Create(AlunoTurma alunoTurma)
        {
            alunoTurma.CreatedAt = DateTimeOffset.Now;

            var sql = @"
                INSERT INTO 
                    AlunoTurma (Ativo, AlunoId, TurmaId, CreatedAt, UpdatedAt)
                VALUES (@Ativo, @AlunoId, @TurmaId, @CreatedAt, null)
            ";

            await _context.Connection.ExecuteAsync(sql, alunoTurma);

            return alunoTurma;
        }

        public async Task<int> Delete(AlunoTurma alunoTurma)
        {
            var sql = "DELETE FROM AlunoTurma WHERE AlunoId = @AlunoId AND TurmaId = @TurmaId";
            var result = await _context.Connection.ExecuteAsync(sql, alunoTurma);

            return result;
        }

        public async Task<List<AlunoTurma>> GetAll()
        {
            var sql = "SELECT * FROM AlunoTurma";
            var alunosTurma = await _context.Connection.QueryAsync<AlunoTurma>(sql);

            return alunosTurma.ToList();
        }

        public async Task<AlunoTurma> GetByAlunoIdTurmaId(int alunoId, int turmaId)
        {
            var sql = "SELECT * FROM AlunoTurma WHERE AlunoId = @AlunoId AND TurmaId = @TurmaId";
            var alunosTurma = await _context.Connection.QuerySingleOrDefaultAsync<AlunoTurma>(sql, new { AlunoId = alunoId, TurmaId = turmaId });

            return alunosTurma;
        }

        public async Task<AlunoTurma> GetByUsuarioIdTurmaId(int usuarioId, int turmaId)
        {
            var sql = @"
                SELECT 
                    at.Id,
                    at.Ativo,
                    at.AlunoId,
                    at.TurmaId
                FROM 
                AlunoTurma AS at 
                INNER JOIN Aluno AS a 
                WHERE a.UsuarioId = @UsuarioId AND at.TurmaId = @TurmaId
            ";

            var alunosTurma = await _context.Connection.QuerySingleOrDefaultAsync<AlunoTurma>(sql, new { UsuarioId = usuarioId, TurmaId = turmaId });

            return alunosTurma;
        }

        public async Task<int> Update(AlunoTurma alunoTurma)
        {
            alunoTurma.UpdatedAt = DateTimeOffset.Now;

            var sql = "UPDATE AlunoTurma SET AlunoId = @AlunoId, TurmaId = @TurmaId, Ativo = @Ativo, CreatedAt = @CreatedAt, UpdatedAt = @UpdatedAt WHERE AlunoId = @AlunoId AND TurmaId = @TurmaId";
            var result = await _context.Connection.ExecuteAsync(sql, alunoTurma);

            return result;
        }
    }
}
