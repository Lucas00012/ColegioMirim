using ColegioMirim.Domain.Usuarios;
using Dapper;

namespace ColegioMirim.Infrastructure.Data.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ColegioMirimContext _context;

        public UsuarioRepository(ColegioMirimContext context)
        {
            _context = context;
        }

        public async Task<int> Count()
        {
            using var connection = _context.BuildConnection();
            var sql = "SELECT COUNT(*) FROM Usuario";
            var count = await connection.QuerySingleAsync<int>(sql);

            return count;
        }

        public async Task<Usuario> Create(Usuario usuario)
        {
            usuario.CreatedAt = DateTimeOffset.Now;

            var sql = @$"
                INSERT INTO 
                    Usuario (Email, SenhaHash, TipoUsuario, CreatedAt, UpdatedAt)
                OUTPUT INSERTED.Id
                VALUES (@Email, @SenhaHash, '{usuario.TipoUsuario}', @CreatedAt, null)
            ";

            using var connection = _context.BuildConnection();
            usuario.Id = await connection.QuerySingleAsync<int>(sql, usuario);

            return usuario;
        }

        public async Task<List<Usuario>> GetAll()
        {
            using var connection = _context.BuildConnection();
            var sql = "SELECT * FROM Usuario";
            var usuario = await connection.QueryAsync<Usuario>(sql);

            return usuario.ToList();
        }

        public async Task<Usuario> GetByEmail(string email)
        {
            using var connection = _context.BuildConnection();
            var sql = "SELECT * FROM Usuario WHERE Email = @Email";
            var usuario = await connection.QuerySingleOrDefaultAsync<Usuario>(sql, new { Email = email });

            return usuario;
        }

        public async Task<Usuario> GetById(int id)
        {
            using var connection = _context.BuildConnection();
            var sql = "SELECT * FROM Usuario WHERE Id = @Id";
            var usuario = await connection.QuerySingleOrDefaultAsync<Usuario>(sql, new { Id = id });

            return usuario;
        }

        public async Task<int> Update(Usuario usuario)
        {
            usuario.UpdatedAt = DateTimeOffset.Now;

            using var connection = _context.BuildConnection();
            var sql = $"UPDATE Usuario SET Email = @Email, SenhaHash = @SenhaHash, TipoUsuario = '{usuario.TipoUsuario}', CreatedAt = @CreatedAt, UpdatedAt = @UpdatedAt WHERE Id = @Id";
            var result = await connection.ExecuteAsync(sql, usuario);

            return result;
        }
    }
}
