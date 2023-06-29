using ColegioMirim.Core.Communication;

namespace ColegioMirim.WebApi.MVC.Models.Response
{
    public class UsuarioClaim
    {
        public string Value { get; set; }
        public string Type { get; set; }
    }

    public class UsuarioRespostaLoginViewModel : ResponseResult
    {
        public string AccessToken { get; set; }
        public double ExpiresIn { get; set; }
        public List<UsuarioClaim> Claims { get; set; }
    }
}
