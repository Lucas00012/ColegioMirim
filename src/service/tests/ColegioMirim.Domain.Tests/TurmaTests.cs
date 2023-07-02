using ColegioMirim.Domain.Turmas.Rules;

namespace ColegioMirim.Domain.Tests
{
    public class TurmaTests
    {
        [Theory(DisplayName = "Nome turma inválido")]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("ab")]
        [InlineData("Engenharia       de software  2")]
        [InlineData("           Engenharia de software 2           ")]
        [InlineData("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        public void NomeTurmaValidoRule_NomeTurmaInvalido_DeveEstarInvalido(string nomeTurma)
        {
            var rule = new NomeTurmaValidoRule(nomeTurma);

            Assert.False(rule.IsValid());
        }

        [Theory(DisplayName = "Nome turma válido")]
        [InlineData("333")]
        [InlineData("abcd")]
        [InlineData("a b c d")]
        [InlineData("Engenharia de software 2")]
        [InlineData("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        public void NomeTurmaValidoRule_NomeTurmaValido_DeveEstarValido(string nomeTurma)
        {
            var rule = new NomeTurmaValidoRule(nomeTurma);

            Assert.True(rule.IsValid());
        }

        [Theory(DisplayName = "Ano turma inválido")]
        [InlineData(0)]
        [InlineData(-2003)]
        [InlineData(2001)]
        [InlineData(10000)]
        public void AnoTurmaValidoRule_AnoTurmaInvalido_DeveEstarInvalido(int anoTurma)
        {
            var rule = new AnoTurmaValidoRule(anoTurma);

            Assert.False(rule.IsValid());
        }

        [Theory(DisplayName = "Ano turma válido")]
        [InlineData(2070)]
        [InlineData(9999)]
        public void AnoTurmaValidoRule_AnoTurmaValido_DeveEstarValido(int anoTurma)
        {
            var rule = new AnoTurmaValidoRule(anoTurma);

            Assert.True(rule.IsValid());
        }
    }
}
