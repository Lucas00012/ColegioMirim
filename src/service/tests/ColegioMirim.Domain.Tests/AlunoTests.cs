using ColegioMirim.Domain.Alunos.Rules;

namespace ColegioMirim.Domain.Tests
{
    public class AlunoTests
    {
        [Theory(DisplayName = "Nome aluno inválido")]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("      a   ")]
        [InlineData("      abc   ")]
        [InlineData("ab")]
        [InlineData("1234567abc")]
        [InlineData("1234567")]
        [InlineData("lucas     eduardo    ormond")]
        [InlineData("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        public void NomeAlunoValidoRule_NomeAlunoInvalido_DeveEstarInvalido(string nomeAluno)
        {
            var rule = new NomeAlunoValidoRule(nomeAluno);

            Assert.False(rule.IsValid());
        }

        [Theory(DisplayName = "Nome aluno válido")]
        [InlineData("ana")]
        [InlineData("lucas eduardo ormond")]
        [InlineData("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        public void NomeAlunoValidoRule_NomeAlunoValido_DeveEstarValido(string nomeAluno)
        {
            var rule = new NomeAlunoValidoRule(nomeAluno);

            Assert.True(rule.IsValid());
        }

        [Theory(DisplayName = "RA aluno inválido")]
        [InlineData("")]
        [InlineData(null)]
        [InlineData(" 1234567 ")]
        [InlineData(" 1234   567 ")]
        [InlineData("abcd123")]
        [InlineData("123456")]
        [InlineData("12345678")]
        [InlineData("aaaaaaa")]
        public void RAValidoRule_RAInvalido_DeveEstarInvalido(string ra)
        {
            var rule = new RAValidoRule(ra);

            Assert.False(rule.IsValid());
        }

        [Theory(DisplayName = "RA aluno válido")]
        [InlineData("1111111")]
        [InlineData("1234567")]
        public void RAValidoRule_RAValido_DeveEstarValido(string ra)
        {
            var rule = new RAValidoRule(ra);

            Assert.True(rule.IsValid());
        }
    }
}