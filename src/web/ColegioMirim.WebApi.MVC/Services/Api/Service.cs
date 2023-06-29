using ColegioMirim.WebApi.MVC.Common.Exceptions;
using ColegioMirim.WebAPI.Core.Identity;
using RestSharp;
using RestSharp.Serializers.Json;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ColegioMirim.WebApi.MVC.Services.Api
{
    public abstract class Service
    {
        private readonly UserSession _userSession;

        protected Service(UserSession userSession)
        {
            _userSession = userSession;
        }

        protected RestClient CreateDefaultClient(string baseUrl)
        {
            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            jsonOptions.Converters.Add(new JsonStringEnumConverter());

            var client = new RestClient(
                baseUrl,
                configureSerialization: s => s.UseSystemTextJson(jsonOptions)
            );

            return client;
        }

        protected void AddBearerToken(RestRequest restRequest)
        {
            var token = _userSession.ExtractClaimValue("JWT");

            if (token is not null)
                restRequest.AddHeader("Authorization", $"Bearer {token}");
        } 

        protected void AssertResponse(RestResponse response)
        {
            switch (response.StatusCode)
            {
                case HttpStatusCode.Unauthorized:
                case HttpStatusCode.Forbidden:
                case HttpStatusCode.NotFound:
                case HttpStatusCode.InternalServerError:
                    throw new CustomHttpRequestException(response.StatusCode);
            }
        }
    }
}
