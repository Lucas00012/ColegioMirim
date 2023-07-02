using ColegioMirim.Application.Commands.AlterarSenha;
using ColegioMirim.Domain.Usuarios.Rules;

namespace ColegioMirim.Application.Tests.AlterarSenha
{
    public class AlterarSenhaCommandTests
    {
        [Fact(DisplayName = "Alterar Senha Command válido")]
        public void AlterarSenhaCommand_CommandValido_DeveSerValido()
        {
            var alterarSenhaCommand = new AlterarSenhaCommand
            {
                SenhaAtual = "@Aa123123",
                NovaSenha = "@Bb123123",
                ConfirmarNovaSenha = "@Bb123123",
            };

            var result = alterarSenhaCommand.EhValido();

            Assert.True(result);
        }

        [Fact(DisplayName = "Alterar Senha Command inválido")]
        public void AlterarSenhaCommand_CommandInvalido_DeveSerInvalido()
        {
            var alterarSenhaCommand = new AlterarSenhaCommand
            {
                SenhaAtual = "",
                NovaSenha = "",
                ConfirmarNovaSenha = "-"
            };

            var result = alterarSenhaCommand.EhValido();
            var errors = alterarSenhaCommand.ValidationResult.Errors.Select(c => c.ErrorMessage).ToList();

            Assert.False(result);
            Assert.Contains("As senhas não conferem", errors);
            Assert.Contains("O campo Senha Atual é obrigatório", errors);
            Assert.Contains(SenhaForteRule.ErrorMessage, errors);
        }
    }
}
