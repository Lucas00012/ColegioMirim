using ColegioMirim.Application.Commands.RegistrarTurma;
using ColegioMirim.Domain.Turmas.Rules;

namespace ColegioMirim.Application.Tests
{
    public class RegistrarTurmaCommandTests
    {
        [Fact(DisplayName = "Registrar Turma Command válido")]
        public void RegistrarTurmaCommand_CommandValido_DeveSerValido()
        {
            // Arrange
            var registrarTurmaCommand = new RegistrarTurmaCommand
            {
                Nome = "Engenharia",
                Ano = DateTime.Today.Year
            };

            // Act
            var result = registrarTurmaCommand.EhValido();

            // Assert
            Assert.True(result);
        }

        [Fact(DisplayName = "Registrar Turma Command inválido")]
        public void RegistrarTurmaCommand_CommandInvalido_DeveSerInvalido()
        {
            // Arrange
            var registrarTurmaCommand = new RegistrarTurmaCommand
            {
                Nome = "",
                Ano = 2001
            };

            // Act
            var result = registrarTurmaCommand.EhValido();
            var errors = registrarTurmaCommand.ValidationResult.Errors.Select(c => c.ErrorMessage).ToList();

            // Assert
            Assert.False(result);
            Assert.Contains(NomeTurmaValidoRule.ErrorMessage, errors);
            Assert.Contains(AnoTurmaValidoRule.ErrorMessage, errors);
        }
    }
}
