﻿using ColegioMirim.WebApi.MVC.Configuration.Settings;
using ColegioMirim.WebApi.MVC.Models;
using ColegioMirim.WebApi.MVC.Models.Response;
using ColegioMirim.WebAPI.Core.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;
using RestSharp;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ColegioMirim.WebApi.MVC.Services.Api
{
    public class UsuariosService : Service
    {
        private readonly BaseUrlsConfiguration _baseUrlsConfiguration;
        private readonly IdentityConfiguration _identityConfiguration;
        private readonly IAuthenticationService _authenticationService;
        private readonly HttpContext _httpContext;

        public UsuariosService(
            IOptions<BaseUrlsConfiguration> baseUrlsConfiguration, 
            IAuthenticationService authenticationService, 
            IHttpContextAccessor httpContextAccessor, 
            IOptions<IdentityConfiguration> identityConfiguration, 
            UserSession userSession) : base(userSession)
        {
            _baseUrlsConfiguration = baseUrlsConfiguration.Value;
            _authenticationService = authenticationService;
            _httpContext = httpContextAccessor.HttpContext;
            _identityConfiguration = identityConfiguration.Value;
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

        public async Task RealizarLogin(UsuarioRespostaLoginViewModel resposta)
        {
            var jwt = new JwtSecurityTokenHandler().ReadToken(resposta.AccessToken) as JwtSecurityToken;

            var claims = jwt.Claims.ToList();
            claims.Add(new Claim("JWT", resposta.AccessToken));

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(_identityConfiguration.ExpiracaoHoras),
                IsPersistent = true
            };

            await _authenticationService.SignInAsync(
                _httpContext,
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }
    }
}