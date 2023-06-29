using ColegioMirim.WebApi.MVC.Common.Exceptions;
using RestSharp;
using RestSharp.Serializers.Json;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ColegioMirim.WebApi.MVC.Services
{
    public abstract class Service
    {
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
