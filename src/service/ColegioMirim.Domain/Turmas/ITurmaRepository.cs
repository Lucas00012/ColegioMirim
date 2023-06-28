namespace ColegioMirim.Domain.Turmas
{
    public interface ITurmaRepository
    {
        Task<List<Turma>> GetAll();
        Task<Turma> GetById(int id);
        Task<Turma> GetByName(string name);
        Task<int> Update(Turma turma);
        Task<int> Count();
        Task<Turma> Create(Turma turma);
    }
}
