using ColegioMirim.WebApi.MVC.Configuration.Settings;
using ColegioMirim.WebApi.MVC.Models;
using ColegioMirim.WebApi.MVC.Models.Response;
using ColegioMirim.WebAPI.Core.Paginator;
using Microsoft.Extensions.Options;
using RestSharp;

namespace ColegioMirim.WebApi.MVC.Services
{
    public class AlunosService : Service
    {
        private readonly BaseUrlsConfiguration _baseUrlsConfiguration;

        public AlunosService(IOptions<BaseUrlsConfiguration> baseUrlsConfiguration)
        {
            _baseUrlsConfiguration = baseUrlsConfiguration.Value;
        }

        public async Task<PaginacaoViewModel<ListarAlunosViewModel>> ListarAlunos(string pesquisa, string orderBy, OrderDirection? direction, int? page, int? pageSize)
        {
            var client = CreateDefaultClient(_baseUrlsConfiguration.ApiColegioMirimUrl);

            var request = new RestRequest("/api/alunos", Method.Get);
            request.AddParameter("pesquisa", pesquisa, ParameterType.QueryString);
            request.AddParameter("orderBy", orderBy, ParameterType.QueryString);
            request.AddParameter("direction", direction, ParameterType.QueryString);
            request.AddParameter("page", page, ParameterType.QueryString);
            request.AddParameter("pageSize", pageSize, ParameterType.QueryString);

            var response = await client.ExecuteAsync<PaginacaoViewModel<ListarAlunosViewModel>>(request);
            AssertResponse(response);

            return response.Data;
        }

        public async Task<ObterAlunoViewModel> ObterAluno(int id)
        {
            var client = CreateDefaultClient(_baseUrlsConfiguration.ApiColegioMirimUrl);

            var request = new RestRequest($"/api/alunos/{id}", Method.Get);

            var response = await client.ExecuteAsync<ObterAlunoViewModel>(request);
            AssertResponse(response);

            return response.Data;
        }

        public async Task<ObterAlunoViewModel> EditarAluno(int id, EditarAlunoViewModel alunoViewModel)
        {
            var client = CreateDefaultClient(_baseUrlsConfiguration.ApiColegioMirimUrl);

            var request = new RestRequest($"/api/alunos/{id}", Method.Put);
            request.AddBody(alunoViewModel);

            var response = await client.ExecuteAsync<ObterAlunoViewModel>(request);
            AssertResponse(response);

            return response.Data;
        }

        public async Task<ObterAlunoViewModel> RegistrarAluno(RegistrarAlunoViewModel alunoViewModel)
        {
            var client = CreateDefaultClient(_baseUrlsConfiguration.ApiColegioMirimUrl);

            var request = new RestRequest("/api/alunos", Method.Post);
            request.AddBody(alunoViewModel);

            var response = await client.ExecuteAsync<ObterAlunoViewModel>(request);
            AssertResponse(response);

            return response.Data;
        }
    }
}
