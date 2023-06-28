using ColegioMirim.Core.DomainObjects;

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

            return _nome.Length >= 3 && _nome.Length <= 60;
        }
    }
}
