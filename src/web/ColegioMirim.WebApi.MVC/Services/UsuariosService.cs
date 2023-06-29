using ColegioMirim.WebApi.MVC.Configuration.Settings;
using ColegioMirim.WebApi.MVC.Models;
using ColegioMirim.WebApi.MVC.Models.Response;
using Microsoft.Extensions.Options;
using RestSharp;

namespace ColegioMirim.WebApi.MVC.Services
{
    public class UsuariosService : Service
    {
        private readonly BaseUrlsConfiguration _baseUrlsConfiguration;

        public UsuariosService(IOptions<BaseUrlsConfiguration> baseUrlsConfiguration)
        {
            _baseUrlsConfiguration = baseUrlsConfiguration.Value;
        }

        public async Task<UsuarioRespostaLoginViewModel> Login(UsuarioLoginViewModel usuarioLogin)
        {
            var client = CreateDefaultClient(_baseUrlsConfiguration.ApiColegioMirimUrl);

            var request = new RestRequest("/api/usuarios/login", Method.Post);
            request.AddBody(usuarioLogin);

            var response = await client.ExecuteAsync<UsuarioRespostaLoginViewModel>(request);
            AssertResponse(response);

            return response.Data;
        }
    }
}
