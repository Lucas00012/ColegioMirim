using ColegioMirim.Core.DomainObjects;

namespace ColegioMirim.Domain.Turmas
{
    public class Turma : Entity, IAggregateRoot
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }
        public int Ano { get; set; }
    }
}
