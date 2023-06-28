using ColegioMirim.Core.DomainObjects;

namespace ColegioMirim.Domain.Usuarios.Rules
{
    public class SenhaForteRule : IBusinessRule
    {
        private readonly string _senha;

        public SenhaForteRule(string senha)
        {
            _senha = senha;
        }

        public static string ErrorMessage => "A senha deve ter no mínimo 8 caracteres e deve conter números, letras maiúsculas, letras minúsculas e símbolos";

        public bool IsValid()
        {
            if (string.IsNullOrEmpty(_senha) || _senha.Length < 8)
                return false;

            if (!_senha.Any(char.IsAsciiLetterLower))
                return false;

            if (!_senha.Any(char.IsAsciiLetterUpper))
                return false;

            if (!_senha.Any(char.IsNumber))
                return false;

            if (!_senha.Any(c => "!@#$%&*()_+-={}/?[]~^´`\"'|<>.;:".Contains(c)))
                return false;

            return true;
        }
    }
}
