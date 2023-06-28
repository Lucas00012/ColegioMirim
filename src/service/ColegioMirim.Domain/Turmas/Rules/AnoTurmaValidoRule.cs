using ColegioMirim.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColegioMirim.Domain.Turmas.Rules
{
    public class AnoTurmaValidoRule : IBusinessRule
    {
        private readonly int _ano;

        public AnoTurmaValidoRule(int ano)
        {
            _ano = ano;
        }

        public static string ErrorMessage => "O ano não pode ser anterior ao atual";

        public bool IsValid()
        {
            return _ano >= DateTime.Today.Year;
        }
    }
}
