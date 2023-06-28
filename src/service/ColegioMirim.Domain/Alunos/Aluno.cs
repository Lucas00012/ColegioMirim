using ColegioMirim.Core.DomainObjects;

namespace ColegioMirim.Domain.Alunos
{
    public class Aluno : Entity, IAggregateRoot
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string RA { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }
    }
}
