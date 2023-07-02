using ColegioMirim.Core.DomainObjects;
using ColegioMirim.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColegioMirim.Domain.Turmas.Rules
{
    public class NomeTurmaValidoRule : IBusinessRule
    {
        private readonly string _nome;

        public NomeTurmaValidoRule(string nome)
        {
            _nome = nome;
        }

        public static string ErrorMessage => "O nome deve ter entre 3 e 60 caracteres";

        public bool IsValid()
        {
            if (string.IsNullOrEmpty(_nome))
                return false;

            if (_nome.Trim() != _nome)
                return false;

            if (_nome.TrimMiddle() != _nome)
                return false;

            return _nome.Length >= 3 && _nome.Length <= 60;
        }
    }
}
