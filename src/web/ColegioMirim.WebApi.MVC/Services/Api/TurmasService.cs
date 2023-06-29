using ColegioMirim.WebApi.MVC.Configuration.Settings;
using ColegioMirim.WebAPI.Core.Paginator;
using ColegioMirim.WebApi.MVC.Models.Response;
using Microsoft.Extensions.Options;
using RestSharp;
using ColegioMirim.WebApi.MVC.Models;
using ColegioMirim.WebAPI.Core.Identity;

namespace ColegioMirim.WebApi.MVC.Services.Api
{
    public class TurmasService : Service
    {
        private readonly BaseUrlsConfiguration _baseUrlsConfiguration;

        public TurmasService(IOptions<BaseUrlsConfiguration> baseUrlsConfiguration, UserSession userSession) : base(userSession)
        {
            _baseUrlsConfiguration = baseUrlsConfiguration.Value;
        }

        public async Task<PaginacaoViewModel<ListarTurmasViewModel>> ListarTurmas(string pesquisa, string orderBy, OrderDirection? direction, int? page, int? pageSize)
        {
            var client = CreateDefaultClient(_baseUrlsConfiguration.ApiColegioMirimUrl);

            var request = new RestRequest("/api/turmas", Method.Get);
            request.AddParameter("pesquisa", pesquisa, ParameterType.QueryString);
            request.AddParameter("orderBy", orderBy, ParameterType.QueryString);
            request.AddParameter("direction", direction, ParameterType.QueryString);
            request.AddParameter("page", page, ParameterType.QueryString);
            request.AddParameter("pageSize", pageSize, ParameterType.QueryString);
            AddBearerToken(request);

            var response = await client.ExecuteAsync<PaginacaoViewModel<ListarTurmasViewModel>>(request);
            AssertResponse(response);

            return response.Data;
        }

        public async Task<ObterTurmaViewModel> ObterTurma(int id)
        {
            var client = CreateDefaultClient(_baseUrlsConfiguration.ApiColegioMirimUrl);

            var request = new RestRequest($"/api/turmas/{id}", Method.Get);
            AddBearerToken(request);

            var response = await client.ExecuteAsync<ObterTurmaViewModel>(request);
            AssertResponse(response);

            return response.Data;
        }

        public async Task<ObterTurmaViewModel> EditarTurma(int id, EditarTurmaViewModel turmaViewModel)
        {
            var client = CreateDefaultClient(_baseUrlsConfiguration.ApiColegioMirimUrl);

            var request = new RestRequest($"/api/turmas/{id}", Method.Put);
            request.AddBody(turmaViewModel);
            AddBearerToken(request);

            var response = await client.ExecuteAsync<ObterTurmaViewModel>(request);
            AssertResponse(response);

            return response.Data;
        }

        public async Task<ObterTurmaViewModel> RegistrarTurma(RegistrarTurmaViewModel turmaViewModel)
        {
            var client = CreateDefaultClient(_baseUrlsConfiguration.ApiColegioMirimUrl);

            var request = new RestRequest("/api/turmas", Method.Post);
            request.AddBody(turmaViewModel);
            AddBearerToken(request);

            var response = await client.ExecuteAsync<ObterTurmaViewModel>(request);
            AssertResponse(response);

            return response.Data;
        }
    }
}
