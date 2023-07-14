using ColegioMirim.Domain.Alunos;
using ColegioMirim.Domain.Turmas;
using Dapper;

namespace ColegioMirim.Infrastructure.Data.Repository
{
    public class TurmaRepository : ITurmaRepository
    {
        private readonly ColegioMirimContext _context;

        public TurmaRepository(ColegioMirimContext context)
        {
            _context = context;
        }

        public async Task<int> Count()
        {
            using var connection = _context.BuildConnection();
            var sql = "SELECT COUNT(*) FROM Turma";
            var count = await connection.QuerySingleAsync<int>(sql);

            return count;
        }

        public async Task<Turma> Create(Turma turma)
        {
            turma.CreatedAt = DateTimeOffset.Now;

            var sql = @"
                INSERT INTO 
                    Turma (Nome, Ativo, Ano, CreatedAt, UpdatedAt)
                OUTPUT INSERTED.Id
                VALUES (@Nome, @Ativo, @Ano, @CreatedAt, null)
            ";

            using var connection = _context.BuildConnection();
            turma.Id = await connection.QuerySingleAsync<int>(sql, turma);

            return turma;
        }

        public async Task<List<Turma>> GetAll()
        {
            using var connection = _context.BuildConnection();
            var sql = "SELECT * FROM Turma";
            var turma = await connection.QueryAsync<Turma>(sql);

            return turma.ToList();
        }

        public async Task<Turma> GetById(int id)
        {
            using var connection = _context.BuildConnection();
            var sql = "SELECT * FROM Turma WHERE Id = @Id";
            var turma = await connection.QuerySingleOrDefaultAsync<Turma>(sql, new { Id = id });

            return turma;
        }

        public async Task<Turma> GetByName(string name)
        {
            using var connection = _context.BuildConnection();
            var sql = "SELECT * FROM Turma WHERE Nome = @Name";
            var turma = await connection.QuerySingleOrDefaultAsync<Turma>(sql, new { Name = name });

            return turma;
        }

        public async Task<int> Update(Turma turma)
        {
            turma.UpdatedAt = DateTimeOffset.Now;

            using var connection = _context.BuildConnection();
            var sql = "UPDATE Turma SET Nome = @Nome, Ativo = @Ativo, Ano = @Ano, CreatedAt = @CreatedAt, UpdatedAt = @UpdatedAt WHERE Id = @Id";
            var result = await connection.ExecuteAsync(sql, turma);

            return result;
        }
    }
}
