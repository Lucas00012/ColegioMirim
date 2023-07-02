using ColegioMirim.Core.DomainObjects;
using ColegioMirim.Core.Extensions;

namespace ColegioMirim.Domain.Alunos.Rules
{
    public class NomeAlunoValidoRule : IBusinessRule
    {
        private readonly string _nome;

        public NomeAlunoValidoRule(string nome)
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

            if (_nome.ApenasNumeros().Any())
                return false;

            if (_nome.TrimMiddle() != _nome)
                return false;

            if (string.IsNullOrEmpty(_nome))
                return false;

            return _nome.Length >= 3 && _nome.Length <= 60;
        }
    }
}
