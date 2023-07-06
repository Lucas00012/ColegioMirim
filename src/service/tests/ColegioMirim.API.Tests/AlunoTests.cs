using ColegioMirim.API.Tests.Setup;
using ColegioMirim.Application.Commands.EditarAluno;
using ColegioMirim.Application.Commands.RegistrarAluno;
using ColegioMirim.Application.DTO;
using ColegioMirim.Base.Tests;
using System.Net;
using System.Net.Http.Json;

namespace ColegioMirim.API.Tests
{
    [TestCaseOrderer("ColegioMirim.Base.Tests.PriorityOrderer", "ColegioMirim.API.Tests")]
    [Collection(nameof(ColegioMirimFixtureCollection))]
    public partial class ApiTests
    {
        private readonly ColegioMirimFixture _fixture;

        public ApiTests(ColegioMirimFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact(DisplayName = "Registrar primeiro aluno"), TestPriority(1)]
        public async Task RegistrarPrimeiroAluno_AlunoRegistrado_DeveRetornarSucesso()
        {
            // Arrange
            var body = new RegistrarAlunoCommand
            {
                Nome = "Lucas Eduardo",
                RA = "111111",
                Email = "lucas@gmail.com",
                Senha = "@Aa123123",
                ConfirmarSenha = "@Aa123123"
            };

            await _fixture.RealizarLogin();

            var client = _fixture.CreateDefaultClient();
            _fixture.AddToken(client);

            // Act
            var response = await client.PostAsJsonAsync("/api/alunos", body);
            var aluno = await _fixture.ReadResponse<AlunoDTO>(response);
            _fixture.AlunosIds.Add(aluno.Id);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact(DisplayName = "Registrar segundo aluno"), TestPriority(2)]
        public async Task RegistrarSegundoAluno_AlunoRegistrado_DeveRetornarSucesso()
        {
            // Arrange
            var body = new RegistrarAlunoCommand
            {
                Nome = "João Silva",
                RA = "111112",
                Email = "joao@gmail.com",
                Senha = "@Aa123123",
                ConfirmarSenha = "@Aa123123"
            };

            await _fixture.RealizarLogin();

            var client = _fixture.CreateDefaultClient();
            _fixture.AddToken(client);

            // Act
            var response = await client.PostAsJsonAsync("/api/alunos", body);
            var aluno = await _fixture.ReadResponse<AlunoDTO>(response);
            _fixture.AlunosIds.Add(aluno.Id);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact(DisplayName = "Editar aluno"), TestPriority(3)]
        public async Task EditarAluno_AlunoEditado_DeveRetornarSucesso()
        {
            // Arrange
            var body = new EditarAlunoCommand
            {
                Nome = "Lucas Eduardo",
                RA = "111111",
                Email = "lucas_eduardo@gmail.com",
                Ativo = true
            };

            await _fixture.RealizarLogin();

            var client = _fixture.CreateDefaultClient();
            _fixture.AddToken(client);

            // Act
            var alunoId = _fixture.AlunosIds[0];
            var response = await client.PutAsJsonAsync($"/api/alunos/{alunoId}", body);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact(DisplayName = "Adicionar aluno email existente"), TestPriority(4)]
        public async Task RegistrarAlunoEmailExistente_EmailRepetido_DeveRetornarErro()
        {
            // Arrange
            var body = new RegistrarAlunoCommand
            {
                Nome = "Lucas Eduardo",
                RA = "111113",
                Email = "lucas_eduardo@gmail.com",
                Senha = "@Aa123123",
                ConfirmarSenha = "@Aa123123"
            };

            await _fixture.RealizarLogin();

            var client = _fixture.CreateDefaultClient();
            _fixture.AddToken(client);

            // Act
            var response = await client.PostAsJsonAsync("/api/alunos", body);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact(DisplayName = "Adicionar aluno RA existente"), TestPriority(5)]
        public async Task RegistrarAlunoRAExistente_RARepetido_DeveRetornarErro()
        {
            // Arrange
            var body = new RegistrarAlunoCommand
            {
                Nome = "Lucas Sousa",
                RA = "111111",
                Email = "lucas_sousa@gmail.com",
                Senha = "@Aa123123",
                ConfirmarSenha = "@Aa123123"
            };

            await _fixture.RealizarLogin();

            var client = _fixture.CreateDefaultClient();
            _fixture.AddToken(client);

            // Act
            var response = await client.PostAsJsonAsync("/api/alunos", body);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact(DisplayName = "Editar aluno email existente"), TestPriority(6)]
        public async Task EditarAlunoEmailExistente_EmailRepetido_DeveRetornarErro()
        {
            // Arrange
            var body = new EditarAlunoCommand
            {
                Nome = "João Silva",
                RA = "111112",
                Email = "lucas_eduardo@gmail.com",
                Ativo = true
            };

            await _fixture.RealizarLogin();

            var client = _fixture.CreateDefaultClient();
            _fixture.AddToken(client);

            // Act
            var alunoId = _fixture.AlunosIds[1];
            var response = await client.PutAsJsonAsync($"/api/alunos/{alunoId}", body);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact(DisplayName = "Editar aluno RA existente"), TestPriority(7)]
        public async Task EditarAlunoRAExistente_RARepetido_DeveRetornarErro()
        {
            // Arrange
            var body = new EditarAlunoCommand
            {
                Nome = "João Silva",
                RA = "111111",
                Email = "joao@gmail.com",
                Ativo = true
            };

            await _fixture.RealizarLogin();

            var client = _fixture.CreateDefaultClient();
            _fixture.AddToken(client);

            // Act
            var alunoId = _fixture.AlunosIds[1];
            var response = await client.PutAsJsonAsync($"/api/alunos/{alunoId}", body);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}