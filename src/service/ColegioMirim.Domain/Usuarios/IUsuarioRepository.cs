namespace ColegioMirim.Domain.Usuarios
{
    public interface IUsuarioRepository
    {
        Task<List<Usuario>> GetAll();
        Task<Usuario> GetById(int id);
        Task<Usuario> GetByEmail(string email);
        Task<int> Update(Usuario usuario);
        Task<int> Count();
        Task<Usuario> Create(Usuario usuario);
    }
}
