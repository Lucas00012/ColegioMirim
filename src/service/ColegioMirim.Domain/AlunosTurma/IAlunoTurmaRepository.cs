namespace ColegioMirim.Domain.AlunosTurma
{
    public interface IAlunoTurmaRepository
    {
        Task<List<AlunoTurma>> GetAll();
        Task<AlunoTurma> GetByAlunoIdTurmaId(int alunoId, int turmaId);
        Task<AlunoTurma> GetByUsuarioIdTurmaId(int usuarioId, int turmaId);
        Task<int> Update(AlunoTurma alunoTurma);
        Task<int> Delete(AlunoTurma alunoTurma);
        Task<int> Count();
        Task<AlunoTurma> Create(AlunoTurma alunoTurma);
    }
}
