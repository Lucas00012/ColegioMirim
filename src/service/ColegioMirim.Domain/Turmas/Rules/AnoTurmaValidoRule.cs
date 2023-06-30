using ColegioMirim.Core.DomainObjects;

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
            return _ano >= DateTime.Today.Year && _ano.ToString().Length == 4;
        }
    }
}
