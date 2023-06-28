using ColegioMirim.Domain.Alunos;
using Dapper;

namespace ColegioMirim.Infrastructure.Data.Repository
{
    public class AlunoRepository : IAlunoRepository
    {
        private readonly ColegioMirimContext _context;

        public AlunoRepository(ColegioMirimContext context)
        {
            _context = context;
        }

        public async Task<int> Count()
        {
            var sql = "SELECT COUNT(*) FROM Aluno";
            var count = await _context.Connection.QuerySingleAsync<int>(sql);

            return count;
        }

        public async Task<Aluno> Create(Aluno aluno)
        {
            aluno.CreatedAt = DateTimeOffset.Now;

            var sql = @"
                INSERT INTO 
                    Aluno (Nome, RA, Ativo, UsuarioId, CreatedAt, UpdatedAt)
                OUTPUT INSERTED.Id
                VALUES (@Nome, @RA, @Ativo, @UsuarioId, @CreatedAt, null)
            ";

            aluno.Id = await _context.Connection.QuerySingleAsync<int>(sql, aluno);

            return aluno;
        }

        public async Task<List<Aluno>> GetAll()
        {
            var sql = "SELECT * FROM Aluno";
            var alunos = await _context.Connection.QueryAsync<Aluno>(sql);

            return alunos.ToList();
        }

        public async Task<Aluno> GetById(int id)
        {
            var sql = "SELECT * FROM Aluno WHERE Id = @Id";
            var aluno = await _context.Connection.QuerySingleOrDefaultAsync<Aluno>(sql, new { Id = id });

            return aluno;
        }

        public async Task<Aluno> GetByRA(string ra)
        {
            var sql = "SELECT * FROM Aluno WHERE RA = @RA";
            var aluno = await _context.Connection.QuerySingleOrDefaultAsync<Aluno>(sql, new { RA = ra });

            return aluno;
        }

        public async Task<Aluno> GetByUsuarioId(int usuarioId)
        {
            var sql = "SELECT * FROM Aluno WHERE UsuarioId = @UsuarioId";
            var aluno = await _context.Connection.QuerySingleOrDefaultAsync<Aluno>(sql, new { UsuarioId = usuarioId });

            return aluno;
        }

        public async Task<int> Update(Aluno aluno)
        {
            aluno.UpdatedAt = DateTimeOffset.Now;

            var sql = "UPDATE Aluno SET Nome = @Nome, RA = @RA, Ativo = @Ativo, UsuarioId = @UsuarioId, CreatedAt = @CreatedAt, UpdatedAt = @UpdatedAt WHERE Id = @Id";
            var result = await _context.Connection.ExecuteAsync(sql, aluno);

            return result;
        }
    }
}
