using Microsoft.AspNetCore.Http;

namespace ColegioMirim.WebAPI.Core.Identity
{
    public interface IUserSession
    {
        int? UsuarioId { get; }
        string Name { get; }
        string Email { get; }
        string Ip { get; }
        string Browser { get; }
        bool IsAuthenticated { get; }
        bool IsAdmin { get; }
        bool IsAluno { get; }
        List<string> Roles { get; }

        string ExtractClaimValue(string type);
    }

    public class UserSession : IUserSession
    {
        private readonly HttpContext _httpContext;

        public UserSession(IHttpContextAccessor httpContextAccessor)
        {
            _httpContext = httpContextAccessor.HttpContext;

            var usuarioId = ExtractClaimValue("sub");
            UsuarioId = string.IsNullOrEmpty(usuarioId) ? null : int.Parse(usuarioId);
            Name = ExtractClaimValue("name");
            Email = ExtractClaimValue("email");
            Roles = ExtractRoles();
            IsAuthenticated = _httpContext?.User.Identity.IsAuthenticated ?? false;
            Browser = _httpContext?.Request.Headers["User-Agent"];
            Ip = _httpContext?.Connection.RemoteIpAddress.ToString();
        }

        public int? UsuarioId { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Ip { get; private set; }
        public string Browser { get; private set; }
        public bool IsAuthenticated { get; private set; }
        public bool IsAdmin => Roles?.Contains("admin") ?? false;
        public bool IsAluno => Roles?.Contains("aluno") ?? false;
        public List<string> Roles { get; private set; }

        public string ExtractClaimValue(string type)
        {
            return _httpContext?.User.Claims
                .FirstOrDefault(c => c.Type == type)?.Value;
        }

        private List<string> ExtractRoles()
        {
            return _httpContext?.User.Claims
                .Where(c => c.Type == "role")
                .Select(c => c.Value)
                .ToList();
        }
    }
}
