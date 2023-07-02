using ColegioMirim.Application.Commands.RealizarLogin;
using ColegioMirim.Domain.Turmas.Rules;
using ColegioMirim.Domain.Usuarios.Rules;

namespace ColegioMirim.Application.Tests
{
    public class RealizarLoginCommandTests
    {
        [Fact(DisplayName = "Realizar Login Command válido")]
        public void RealizarLoginCommand_CommandValido_DeveSerValido()
        {
            // Arrange
            var realizarLoginCommand = new RealizarLoginCommand
            {
                Email = "lucas@gmail.com",
                Senha = "@Aa123123"
            };

            // Act
            var result = realizarLoginCommand.EhValido();

            // Assert
            Assert.True(result);
        }

        [Fact(DisplayName = "Realizar Login Command inválido")]
        public void RealizarLoginCommand_CommandInvalido_DeveSerInvalido()
        {
            // Arrange
            var realizarLoginCommand = new RealizarLoginCommand
            {
                Email = "",
                Senha = ""
            };

            // Act
            var result = realizarLoginCommand.EhValido();
            var errors = realizarLoginCommand.ValidationResult.Errors.Select(c => c.ErrorMessage).ToList();

            // Assert
            Assert.False(result);
            Assert.Contains(EmailValidoRule.ErrorMessage, errors);
            Assert.Contains("O campo Senha é obrigatório", errors);
        }
    }
}
