using ColegioMirim.Application.Commands.EditarAluno;
using ColegioMirim.Domain.Alunos.Rules;

namespace ColegioMirim.Application.Tests
{
    public class EditarAlunoCommandTests
    {
        [Fact(DisplayName = "Editar Aluno Command válido")]
        public void EditarAlunoCommand_CommandValido_DeveSerValido()
        {
            // Arrange
            var editarAlunoCommand = new EditarAlunoCommand
            {
                Email = "lucas@gmail.com",
                Nome = "Lucas",
                RA = "1111111"
            };

            // Act
            var result = editarAlunoCommand.EhValido();

            // Assert
            Assert.True(result);
        }

        [Fact(DisplayName = "Editar Aluno Command inválido")]
        public void EditarAlunoCommand_CommandInvalido_DeveSerInvalido()
        {
            // Arrange
            var editarAlunoCommand = new EditarAlunoCommand
            {
                Email = "",
                Nome = "",
                RA = ""
            };

            // Act
            var result = editarAlunoCommand.EhValido();
            var errors = editarAlunoCommand.ValidationResult.Errors.Select(c => c.ErrorMessage).ToList();

            // Assert
            Assert.False(result);
            Assert.Contains(NomeAlunoValidoRule.ErrorMessage, errors);
            Assert.Contains(RAValidoRule.ErrorMessage, errors);
            Assert.Contains(NomeAlunoValidoRule.ErrorMessage, errors);
        }
    }
}
