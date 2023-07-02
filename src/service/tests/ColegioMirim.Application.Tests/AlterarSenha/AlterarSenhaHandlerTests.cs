using ColegioMirim.Application.Commands.AlterarSenha;
using ColegioMirim.Domain.Usuarios;
using ColegioMirim.WebAPI.Core.Identity;
using Moq;
using Moq.AutoMock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColegioMirim.Application.Tests.AlterarSenha
{
    public class AlterarSenhaHandlerTests
    {
        private readonly AutoMocker _mocker;
        private readonly AlterarSenhaHandler _alterarSenhaHandler;

        public AlterarSenhaHandlerTests()
        {
            _mocker = new AutoMocker();
            _alterarSenhaHandler = _mocker.CreateInstance<AlterarSenhaHandler>();
        }

        [Fact(DisplayName = "Alterar Senha Handler - Command válido")]
        public async Task AlterarSenha_CommandValido_DeveExecutarComSucesso()
        {
            var alterarSenhaCommand = new AlterarSenhaCommand
            {
                SenhaAtual = "@Aa123123",
                NovaSenha = "@Bb123123",
                ConfirmarNovaSenha = "@Bb123123",
            };

            _mocker.GetMock<UserSession>().Setup(r => r.UsuarioId).Returns(0);

            var result = await _alterarSenhaHandler.Handle(alterarSenhaCommand, CancellationToken.None);

            Assert.True(result.Success);
            _mocker.GetMock<IUsuarioRepository>().Verify(r => r.Update(It.IsAny<Usuario>()), Times.Once);
        }
    }
}
