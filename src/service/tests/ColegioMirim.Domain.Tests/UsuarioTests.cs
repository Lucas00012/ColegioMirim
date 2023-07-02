using ColegioMirim.Domain.Usuarios.Rules;

namespace ColegioMirim.Domain.Tests
{
    public class UsuarioTests
    {
        [Theory(DisplayName = "Email inválido")]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("joao")]
        [InlineData("   joao@gmail.com     ")]
        [InlineData("   joao@    gmail.com     ")]
        [InlineData("joao@    gmail.com")]
        public void EmailValidoRule_EmailInvalido_DeveEstarInvalido(string email)
        {
            // Arrange
            var rule = new EmailValidoRule(email);

            // Act & Assert
            Assert.False(rule.IsValid());
        }

        [Theory(DisplayName = "Email válido")]
        [InlineData("joao@gmail.com")]
        [InlineData("joao@gmail")]
        [InlineData("a@a")]
        [InlineData("43456674@gmail.com")]
        public void EmailValidoRule_EmailValido_DeveEstarValido(string email)
        {
            // Arrange
            var rule = new EmailValidoRule(email);

            // Act & Assert
            Assert.True(rule.IsValid());
        }

        [Theory(DisplayName = "Senha inválida")]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("123456")]
        [InlineData("12345678")]
        [InlineData("@Aa1234")]
        [InlineData("Aa12345678")]
        [InlineData("@_+__?:234564")]
        [InlineData("aaaaaaaaaa")]
        [InlineData("AAAAAAAAAA")]
        [InlineData("@@@@@@@@@@")]
        public void SenhaForteRule_SenhaInvalida_DeveEstarInvalida(string senha)
        {
            // Arrange
            var rule = new SenhaForteRule(senha);

            // Act & Assert
            Assert.False(rule.IsValid());
        }

        [Theory(DisplayName = "Senha válida")]
        [InlineData("@Aa123456")]
        [InlineData("@Aa12345")]
        [InlineData(" Aa12345")]
        public void SenhaForteRule_SenhaValida_DeveEstarValida(string senha)
        {
            // Arrange
            var rule = new SenhaForteRule(senha);

            // Act & Assert
            Assert.True(rule.IsValid());
        }
    }
}
