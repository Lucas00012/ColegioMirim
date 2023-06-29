using ColegioMirim.Core.Communication;

namespace ColegioMirim.WebApi.MVC.Models.Response
{
    public class UsuarioRespostaLoginViewModel : ResponseResult
    {
        public string AccessToken { get; set; }
        public double ExpiresIn { get; set; }
    }
}
