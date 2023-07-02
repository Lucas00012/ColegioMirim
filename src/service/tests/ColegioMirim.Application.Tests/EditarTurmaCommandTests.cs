using ColegioMirim.Application.Commands.EditarAluno;
using ColegioMirim.Application.Commands.EditarTurma;
using ColegioMirim.Domain.Alunos.Rules;
using ColegioMirim.Domain.Turmas.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColegioMirim.Application.Tests
{
    public class EditarTurmaCommandTests
    {
        [Fact(DisplayName = "Editar Turma Command válido")]
        public void EditarTurmaCommand_CommandValido_DeveSerValido()
        {
            // Arrange
            var editarTurmaCommand = new EditarTurmaCommand
            {
                Nome = "Engenharia",
                Ano = DateTime.Today.Year
            };

            // Act
            var result = editarTurmaCommand.EhValido();

            // Assert
            Assert.True(result);
        }

        [Fact(DisplayName = "Editar Turma Command inválido")]
        public void EditarTurmaCommand_CommandInvalido_DeveSerInvalido()
        {
            // Arrange
            var editarTurmaCommand = new EditarTurmaCommand
            {
                Nome = "",
                Ano = 2001
            };

            // Act
            var result = editarTurmaCommand.EhValido();
            var errors = editarTurmaCommand.ValidationResult.Errors.Select(c => c.ErrorMessage).ToList();

            // Assert
            Assert.False(result);
            Assert.Contains(NomeTurmaValidoRule.ErrorMessage, errors);
            Assert.Contains(AnoTurmaValidoRule.ErrorMessage, errors);
        }
    }
}
