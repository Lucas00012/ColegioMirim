using ColegioMirim.API.Tests.Setup;
using ColegioMirim.Application.Commands.EditarAluno;
using ColegioMirim.Application.Commands.RegistrarAluno;
using ColegioMirim.Application.DTO;
using ColegioMirim.Testing.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ColegioMirim.Application.Commands.RegistrarTurma;
using ColegioMirim.Application.Commands.EditarTurma;

namespace ColegioMirim.API.Tests
{
    public partial class ApiTests
    {
        [Fact(DisplayName = "Registrar primeira turma"), TestPriority(8)]
        public async Task RegistrarPrimeiraTurma_TurmaRegistrada_DeveRetornarSucesso()
        {
            // Arrange
            var body = new RegistrarTurmaCommand
            {
                Nome = "Engenharia",
                Ano = 2089
            };

            await _fixture.RealizarLogin();

            var client = _fixture.CreateDefaultClient();
            _fixture.AddToken(client);

            // Act
            var response = await client.PostAsJsonAsync("/api/turmas", body);
            var turma = await _fixture.ReadResponse<TurmaDTO>(response);
            _fixture.TurmasIds.Add(turma.Id);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact(DisplayName = "Registrar segunda turma"), TestPriority(9)]
        public async Task RegistrarSegundaTurma_TurmaRegistrada_DeveRetornarSucesso()
        {
            // Arrange
            var body = new RegistrarTurmaCommand
            {
                Nome = "Física",
                Ano = 2089
            };

            await _fixture.RealizarLogin();

            var client = _fixture.CreateDefaultClient();
            _fixture.AddToken(client);

            // Act
            var response = await client.PostAsJsonAsync("/api/turmas", body);
            var turma = await _fixture.ReadResponse<TurmaDTO>(response);
            _fixture.TurmasIds.Add(turma.Id);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact(DisplayName = "Editar turma"), TestPriority(10)]
        public async Task EditarTurma_TurmaEditada_DeveRetornarSucesso()
        {
            // Arrange
            var body = new EditarTurmaCommand
            {
                Nome = "Engenharia II",
                Ano = 2089,
                Ativo = true
            };

            await _fixture.RealizarLogin();

            var client = _fixture.CreateDefaultClient();
            _fixture.AddToken(client);

            // Act
            var turmaId = _fixture.TurmasIds[0];
            var response = await client.PutAsJsonAsync($"/api/turmas/{turmaId}", body);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact(DisplayName = "Registrar turma nome existente"), TestPriority(11)]
        public async Task RegistrarTurmaNomeExistente_NomeRepetido_DeveRetornarErro()
        {
            // Arrange
            var body = new RegistrarTurmaCommand
            {
                Nome = "Engenharia II",
                Ano = 2089
            };

            await _fixture.RealizarLogin();

            var client = _fixture.CreateDefaultClient();
            _fixture.AddToken(client);

            // Act
            var response = await client.PostAsJsonAsync($"/api/turmas", body);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact(DisplayName = "Editar turma nome existente"), TestPriority(12)]
        public async Task EditarTurmaNomeExistente_NomeRepetido_DeveRetornarErro()
        {
            // Arrange
            var body = new EditarTurmaCommand
            {
                Nome = "Engenharia II",
                Ano = 2089,
                Ativo = true
            };

            await _fixture.RealizarLogin();

            var client = _fixture.CreateDefaultClient();
            _fixture.AddToken(client);

            // Act
            var turmaId = _fixture.TurmasIds[1];
            var response = await client.PutAsJsonAsync($"/api/turmas/{turmaId}", body);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
