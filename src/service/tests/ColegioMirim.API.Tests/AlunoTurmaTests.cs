using ColegioMirim.Application.Commands.EditarAlunoTurma;
using ColegioMirim.Application.Commands.RegistrarAlunoTurma;
using ColegioMirim.Application.Commands.RegistrarTurma;
using ColegioMirim.Application.DTO;
using ColegioMirim.Testing.Common;
using System.Net;
using System.Net.Http.Json;

namespace ColegioMirim.API.Tests
{
    public partial class ApiTests
    {
        [Fact(DisplayName = "Registrar primeiro vinculo (aluno/turma)"), TestPriority(13)]
        public async Task RegistrarPrimeiroVinculo_VinculoRegistrado_DeveRetornarSucesso()
        {
            // Arrange
            var body = new RegistrarAlunoTurmaCommand
            {
                AlunoId = _fixture.AlunosIds[0],
                TurmaId = _fixture.TurmasIds[0]
            };

            await _fixture.RealizarLogin();

            var client = _fixture.CreateDefaultClient();
            _fixture.AddToken(client);

            // Act
            var response = await client.PostAsJsonAsync("/api/alunos-turma", body);
            var vinculo = await _fixture.ReadResponse<TurmaDTO>(response);
            _fixture.VinculosIds.Add(vinculo.Id);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact(DisplayName = "Registrar segundo vinculo (aluno/turma)"), TestPriority(14)]
        public async Task RegistrarSegundoVinculo_VinculoRegistrado_DeveRetornarSucesso()
        {
            // Arrange
            var body = new RegistrarAlunoTurmaCommand
            {
                AlunoId = _fixture.AlunosIds[1],
                TurmaId = _fixture.TurmasIds[1]
            };

            await _fixture.RealizarLogin();

            var client = _fixture.CreateDefaultClient();
            _fixture.AddToken(client);

            // Act
            var response = await client.PostAsJsonAsync("/api/alunos-turma", body);
            var vinculo = await _fixture.ReadResponse<TurmaDTO>(response);
            _fixture.VinculosIds.Add(vinculo.Id);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact(DisplayName = "Registrar vinculo já existente (aluno/turma)"), TestPriority(15)]
        public async Task RegistrarVinculoJaExistente_VinculoExistente_DeveRetornarErro()
        {
            // Arrange
            var body = new RegistrarAlunoTurmaCommand
            {
                AlunoId = _fixture.AlunosIds[1],
                TurmaId = _fixture.TurmasIds[1]
            };

            await _fixture.RealizarLogin();

            var client = _fixture.CreateDefaultClient();
            _fixture.AddToken(client);

            // Act
            var response = await client.PostAsJsonAsync("/api/alunos-turma", body);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact(DisplayName = "Editar vinculo para um já existente (aluno/turma)"), TestPriority(16)]
        public async Task RegistrarVinculoParaUmJaExistente_VinculoExistente_DeveRetornarErro()
        {
            // Arrange
            var body = new EditarAlunoTurmaCommand
            {
                AlunoId = _fixture.AlunosIds[0],
                TurmaId = _fixture.TurmasIds[0],
                Ativo = true
            };

            await _fixture.RealizarLogin();

            var client = _fixture.CreateDefaultClient();
            _fixture.AddToken(client);

            // Act
            var vinculoId = _fixture.VinculosIds[1];
            var response = await client.PutAsJsonAsync($"/api/alunos-turma/{vinculoId}", body);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
