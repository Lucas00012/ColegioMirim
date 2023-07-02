using ColegioMirim.Application.Commands.AlterarSenha;
using ColegioMirim.Domain.Usuarios.Rules;

namespace ColegioMirim.Application.Tests
{
    public class AlterarSenhaCommandTests
    {
        [Fact(DisplayName = "Alterar Senha Command válido")]
        public void AlterarSenhaCommand_CommandValido_DeveSerValido()
        {
            // Arrange
            var alterarSenhaCommand = new AlterarSenhaCommand
            {
                SenhaAtual = "@Aa123123",
                NovaSenha = "@Bb123123",
                ConfirmarNovaSenha = "@Bb123123",
            };

            // Act
            var result = alterarSenhaCommand.EhValido();

            // Assert
            Assert.True(result);
        }

        [Fact(DisplayName = "Alterar Senha Command inválido")]
        public void AlterarSenhaCommand_CommandInvalido_DeveSerInvalido()
        {
            // Arrange
            var alterarSenhaCommand = new AlterarSenhaCommand
            {
                SenhaAtual = "",
                NovaSenha = "",
                ConfirmarNovaSenha = "-"
            };

            // Act
            var result = alterarSenhaCommand.EhValido();
            var errors = alterarSenhaCommand.ValidationResult.Errors.Select(c => c.ErrorMessage).ToList();

            // Assert
            Assert.False(result);
            Assert.Contains("As senhas não conferem", errors);
            Assert.Contains("O campo Senha Atual é obrigatório", errors);
            Assert.Contains(SenhaForteRule.ErrorMessage, errors);
        }
    }
}
