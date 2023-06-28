namespace ColegioMirim.Domain.Alunos
{
    public interface IAlunoRepository
    {
        Task<List<Aluno>> GetAll();
        Task<Aluno> GetById(int id);
        Task<Aluno> GetByRA(string ra);
        Task<Aluno> GetByUsuarioId(int usuarioId);
        Task<int> Update(Aluno aluno);
        Task<int> Count();
        Task<Aluno> Create(Aluno aluno);
    }
}
