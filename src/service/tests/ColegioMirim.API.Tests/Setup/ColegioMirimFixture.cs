using ColegioMirim.Application.Commands.RealizarLogin;
using ColegioMirim.Application.Services.JwtToken.Models;
using ColegioMirim.Infrastructure.Data;
using Dapper;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ColegioMirim.API.Tests.Setup
{
    [CollectionDefinition(nameof(ColegioMirimFixtureCollection))]
    public class ColegioMirimFixtureCollection : ICollectionFixture<ColegioMirimFixture>
    {

    }

    public class ColegioMirimFixture : IDisposable
    {
        private readonly ColegioMirimFactory _factory;
        private string _token;

        public List<int> AlunosIds { get; set; } = new();
        public List<int> TurmasIds { get; set; } = new();
        public List<int> VinculosIds { get; set; } = new();

        public ColegioMirimFixture()
        {
            _factory = new ColegioMirimFactory();
        }

        public async Task RealizarLogin()
        {
            var body = new RealizarLoginCommand
            {
                Email = "admin@colegiomirim.com",
                Senha = "@Aa123456"
            };

            var client = CreateDefaultClient();

            var response = await client.PostAsJsonAsync("api/usuarios/login", body);
            response.EnsureSuccessStatusCode();

            var data = await ReadResponse<JwtTokenResult>(response);
            _token = data.AccessToken;
        }

        public async Task<T> ReadResponse<T>(HttpResponseMessage message)
        {
            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            jsonOptions.Converters.Add(new JsonStringEnumConverter());

            return JsonSerializer.Deserialize<T>(await message.Content.ReadAsStringAsync(), jsonOptions);
        }

        public HttpClient CreateDefaultClient()
        {
            var httpClient = _factory.CreateClient();

            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return httpClient;
        }

        public void AddToken(HttpClient httpClient)
        {
            if (!string.IsNullOrEmpty(_token))
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
        }

        public void Dispose()
        {
            var context = _factory.Services.GetService<ColegioMirimContext>();

            context.Connection.Execute(@"
                delete from AlunoTurma
                delete from Aluno
                delete from Turma
                delete from Usuario where TipoUsuario != 'Administrador'
            ");
        }
    }
}
