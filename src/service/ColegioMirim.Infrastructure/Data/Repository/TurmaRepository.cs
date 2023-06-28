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
            var sql = "SELECT COUNT(*) FROM Turma";
            var count = await _context.Connection.QuerySingleAsync<int>(sql);

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

            turma.Id = await _context.Connection.QuerySingleAsync<int>(sql, turma);
            return turma;
        }

        public async Task<List<Turma>> GetAll()
        {
            var sql = "SELECT * FROM Turma";
            var turma = await _context.Connection.QueryAsync<Turma>(sql);

            return turma.ToList();
        }

        public async Task<Turma> GetById(int id)
        {
            var sql = "SELECT * FROM Turma WHERE Id = @Id";
            var turma = await _context.Connection.QuerySingleOrDefaultAsync<Turma>(sql, new { Id = id });

            return turma;
        }

        public async Task<Turma> GetByName(string name)
        {
            var sql = "SELECT * FROM Turma WHERE Nome = @Name";
            var turma = await _context.Connection.QuerySingleOrDefaultAsync<Turma>(sql, new { Name = name });

            return turma;
        }

        public async Task<int> Update(Turma turma)
        {
            turma.UpdatedAt = DateTimeOffset.Now;

            var sql = "UPDATE Turma SET Nome = @Nome, Ativo = @Ativo, Ano = @Ano, CreatedAt = @CreatedAt, UpdatedAt = @UpdatedAt WHERE Id = @Id";
            var result = await _context.Connection.ExecuteAsync(sql, turma);

            return result;
        }
    }
}
