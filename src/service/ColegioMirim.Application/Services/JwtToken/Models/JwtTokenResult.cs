namespace ColegioMirim.Application.Services.JwtToken.Models
{
    public class JwtTokenResult
    {
        public string AccessToken { get; set; }
        public double ExpiresIn { get; set; }
    }
}
