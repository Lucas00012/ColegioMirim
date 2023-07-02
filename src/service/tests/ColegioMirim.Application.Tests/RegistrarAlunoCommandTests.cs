using ColegioMirim.Application.Commands.RealizarLogin;
using ColegioMirim.Application.Commands.RegistrarAluno;
using ColegioMirim.Domain.Alunos.Rules;
using ColegioMirim.Domain.Usuarios.Rules;

namespace ColegioMirim.Application.Tests
{
    public class RegistrarAlunoCommandTests
    {
        [Fact(DisplayName = "Registrar Aluno Command válido")]
        public void RegistrarAlunoCommand_CommandValido_DeveSerValido()
        {
            // Arrange
            var registrarAlunoCommand = new RegistrarAlunoCommand
            {
                Email = "lucas@gmail.com",
                Nome = "Lucas",
                RA = "1111111",
                Senha = "@Aa123123",
                ConfirmarSenha = "@Aa123123"
            };

            // Act
            var result = registrarAlunoCommand.EhValido();

            // Assert
            Assert.True(result);
        }

        [Fact(DisplayName = "Registrar Aluno Command inválido")]
        public void RealizarLoginCommand_CommandInvalido_DeveSerInvalido()
        {
            // Arrange
            var registrarAlunoCommand = new RegistrarAlunoCommand
            {
                ConfirmarSenha = "-"
            };

            // Act
            var result = registrarAlunoCommand.EhValido();
            var errors = registrarAlunoCommand.ValidationResult.Errors.Select(c => c.ErrorMessage).ToList();

            // Assert
            Assert.False(result);
            Assert.Contains(NomeAlunoValidoRule.ErrorMessage, errors);
            Assert.Contains(RAValidoRule.ErrorMessage, errors);
            Assert.Contains(EmailValidoRule.ErrorMessage, errors);
            Assert.Contains(SenhaForteRule.ErrorMessage, errors);
            Assert.Contains("As senhas não conferem", errors);
        }
    }
}
