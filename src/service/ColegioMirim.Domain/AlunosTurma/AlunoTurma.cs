using ColegioMirim.Core.DomainObjects;

namespace ColegioMirim.Domain.AlunosTurma
{
    public class AlunoTurma : Entity, IAggregateRoot
    {
        public int Id { get; set; }
        public bool Ativo { get; set; }
        public int AlunoId { get; set; }
        public int TurmaId { get; set; }
    }
}
