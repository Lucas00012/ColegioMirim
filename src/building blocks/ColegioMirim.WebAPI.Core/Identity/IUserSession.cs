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
}
