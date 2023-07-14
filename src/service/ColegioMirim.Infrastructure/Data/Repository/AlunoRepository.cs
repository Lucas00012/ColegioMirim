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
            using var connection = _context.BuildConnection();
            var sql = "SELECT COUNT(*) FROM Aluno";
            var count = await connection.QuerySingleAsync<int>(sql);

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

            using var connection = _context.BuildConnection();
            aluno.Id = await connection.QuerySingleAsync<int>(sql, aluno);

            return aluno;
        }

        public async Task<List<Aluno>> GetAll()
        {
            var sql = "SELECT * FROM Aluno";

            using var connection = _context.BuildConnection();
            var alunos = await connection.QueryAsync<Aluno>(sql);

            return alunos.ToList();
        }

        public async Task<Aluno> GetById(int id)
        {
            var sql = "SELECT * FROM Aluno WHERE Id = @Id";

            using var connection = _context.BuildConnection();
            var aluno = await connection.QuerySingleOrDefaultAsync<Aluno>(sql, new { Id = id });

            return aluno;
        }

        public async Task<Aluno> GetByRA(string ra)
        {
            var sql = "SELECT * FROM Aluno WHERE RA = @RA";

            using var connection = _context.BuildConnection();
            var aluno = await connection.QuerySingleOrDefaultAsync<Aluno>(sql, new { RA = ra });

            return aluno;
        }

        public async Task<Aluno> GetByUsuarioId(int usuarioId)
        {
            var sql = "SELECT * FROM Aluno WHERE UsuarioId = @UsuarioId";

            using var connection = _context.BuildConnection();
            var aluno = await connection.QuerySingleOrDefaultAsync<Aluno>(sql, new { UsuarioId = usuarioId });

            return aluno;
        }

        public async Task<int> Update(Aluno aluno)
        {
            aluno.UpdatedAt = DateTimeOffset.Now;

            using var connection = _context.BuildConnection();
            var sql = "UPDATE Aluno SET Nome = @Nome, RA = @RA, Ativo = @Ativo, UsuarioId = @UsuarioId, CreatedAt = @CreatedAt, UpdatedAt = @UpdatedAt WHERE Id = @Id";
            var result = await connection.ExecuteAsync(sql, aluno);

            return result;
        }
    }
}
