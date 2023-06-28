using ColegioMirim.API.Services.JwtToken.Models;
using ColegioMirim.Domain.Alunos;
using ColegioMirim.Domain.Usuarios;
using ColegioMirim.WebAPI.Core.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ColegioMirim.API.Services.JwtToken
{
    public class JwtTokenService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IAlunoRepository _alunoRepository;
        private readonly IdentityConfiguration _identityConfiguration;

        public JwtTokenService(IUsuarioRepository usuarioRepository, IOptions<IdentityConfiguration> identityConfiguration, IAlunoRepository alunoRepository)
        {
            _usuarioRepository = usuarioRepository;
            _identityConfiguration = identityConfiguration.Value;
            _alunoRepository = alunoRepository;
        }

        public async Task<JwtTokenResult> GerarJwt(string email, string senha)
        {
            var usuario = await _usuarioRepository.GetByEmail(email);
            if (usuario is null)
            {
                return null;
            }

            if (usuario.SenhaHash != Usuario.GerarSenhaHash(senha))
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var claims = await ObterClaims(usuario);

            var tokenSecurity = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _identityConfiguration.Issuer,
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(_identityConfiguration.ExpiracaoHoras),
                SigningCredentials = new SigningCredentials(_identityConfiguration.Key, SecurityAlgorithms.HmacSha256Signature)
            });

            var tokenString = tokenHandler.WriteToken(tokenSecurity);

            var response = new JwtTokenResult
            {
                AccessToken = tokenString,
                ExpiresIn = TimeSpan.FromHours(_identityConfiguration.ExpiracaoHoras).TotalSeconds
            };

            return response;
        }

        private async Task<IEnumerable<Claim>> ObterClaims(Usuario usuario)
        {
            var aluno = await _alunoRepository.GetByUsuarioId(usuario.Id);

            var role = usuario.TipoUsuario switch
            {
                TipoUsuario.Administrador => "admin",
                TipoUsuario.Aluno => "aluno",
                TipoUsuario.Professor => "professor",
                _ => throw new ArgumentOutOfRangeException()
            };

            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, usuario.Id.ToString()),
                new(JwtRegisteredClaimNames.Email, usuario.Email),
                new(JwtRegisteredClaimNames.Name, aluno?.Nome ?? usuario.Email),
                new(JwtRegisteredClaimNames.Iat, DateTime.UnixEpoch.ToString()),
                new(JwtRegisteredClaimNames.Nbf, DateTime.UnixEpoch.ToString()),
                new(JwtRegisteredClaimNames.Aud, _identityConfiguration.Audience),
                new("role", role)
            };

            return claims;
        }
    }
}
