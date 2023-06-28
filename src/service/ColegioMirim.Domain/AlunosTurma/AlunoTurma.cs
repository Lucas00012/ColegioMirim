using ColegioMirim.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColegioMirim.Domain.AlunosTurma
{
    public class AlunoTurma : Entity, IAggregateRoot
    {
        public bool Ativo { get; set; }
        public int AlunoId { get; set; }
        public int TurmaId { get; set; }
    }
}
