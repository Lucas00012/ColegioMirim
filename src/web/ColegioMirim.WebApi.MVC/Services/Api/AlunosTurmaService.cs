﻿using ColegioMirim.WebApi.MVC.Configuration.Settings;
using ColegioMirim.WebAPI.Core.Paginator;
using ColegioMirim.WebApi.MVC.Models.Response;
using ColegioMirim.WebApi.MVC.Models;
using Microsoft.Extensions.Options;
using RestSharp;
using ColegioMirim.Core.Communication;
using ColegioMirim.WebAPI.Core.Identity;

namespace ColegioMirim.WebApi.MVC.Services.Api
{
    public class AlunosTurmaService : Service
    {
        private readonly BaseUrlsConfiguration _baseUrlsConfiguration;

        public AlunosTurmaService(IOptions<BaseUrlsConfiguration> baseUrlsConfiguration, IUserSession userSession) : base(userSession)
        {
            _baseUrlsConfiguration = baseUrlsConfiguration.Value;
        }

        public async Task<PaginacaoViewModel<ListarAlunosTurmaViewModel>> ListarAlunosTurma(string pesquisa, string orderBy, OrderDirection? direction, int? page, int? pageSize)
        {
            var client = CreateDefaultClient(_baseUrlsConfiguration.ApiColegioMirimUrl);

            var request = new RestRequest("/api/alunos-turma", Method.Get);
            request.AddParameter("pesquisa", pesquisa, ParameterType.QueryString);
            request.AddParameter("orderBy", orderBy, ParameterType.QueryString);
            request.AddParameter("direction", direction, ParameterType.QueryString);
            request.AddParameter("page", page, ParameterType.QueryString);
            request.AddParameter("pageSize", pageSize, ParameterType.QueryString);
            AddBearerToken(request);

            var response = await client.ExecuteAsync<PaginacaoViewModel<ListarAlunosTurmaViewModel>>(request);
            AssertResponse(response);

            return response.Data;
        }

        public async Task<ObterAlunoTurmaViewModel> ObterAlunoTurma(int id)
        {
            var client = CreateDefaultClient(_baseUrlsConfiguration.ApiColegioMirimUrl);

            var request = new RestRequest($"/api/alunos-turma/{id}", Method.Get);
            AddBearerToken(request);

            var response = await client.ExecuteAsync<ObterAlunoTurmaViewModel>(request);
            AssertResponse(response);

            return response.Data;
        }

        public async Task<ObterAlunoTurmaViewModel> EditarAlunoTurma(int id, EditarAlunoTurmaViewModel model)
        {
            var client = CreateDefaultClient(_baseUrlsConfiguration.ApiColegioMirimUrl);

            var request = new RestRequest($"/api/alunos-turma/{id}", Method.Put);
            request.AddBody(model);
            AddBearerToken(request);

            var response = await client.ExecuteAsync<ObterAlunoTurmaViewModel>(request);
            AssertResponse(response);

            return response.Data;
        }

        public async Task<ObterAlunoViewModel> RegistrarAlunoTurma(RegistrarAlunoTurmaViewModel model)
        {
            var client = CreateDefaultClient(_baseUrlsConfiguration.ApiColegioMirimUrl);

            var request = new RestRequest("/api/alunos-turma", Method.Post);
            request.AddBody(model);
            AddBearerToken(request);

            var response = await client.ExecuteAsync<ObterAlunoViewModel>(request);
            AssertResponse(response);

            return response.Data;
        }

        public async Task<ResponseResult> RemoverAlunoTurma(int id)
        {
            var client = CreateDefaultClient(_baseUrlsConfiguration.ApiColegioMirimUrl);

            var request = new RestRequest($"/api/alunos-turma/{id}", Method.Delete);
            AddBearerToken(request);

            var response = await client.ExecuteAsync<ResponseResult>(request);
            AssertResponse(response);

            return response.Data;
        }
    }
}
