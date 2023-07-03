using ColegioMirim.Application.Commands.RealizarLogin;
using ColegioMirim.Application.Services.JwtToken.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ColegioMirim.API.Tests.Setup
{
    public class ColegioMirimFixture
    {
        private readonly TestServer _server;
        private string _token;

        public ColegioMirimFixture()
        {
            _server = new TestServer(new WebHostBuilder()
                .UseStartup<TestStartup>()
                .UseEnvironment("Testing"));
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

        public static async Task<T> ReadResponse<T>(HttpResponseMessage message)
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
            var httpClient = _server.CreateClient();

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
            _server.Dispose();
        }
    }
}
