using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ColegioMirim.WebAPI.Core.Identity
{
    public class IdentityConfiguration
    {
        public int ExpiracaoHoras { get; set; }
        public string Secret { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public SymmetricSecurityKey Key => new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Secret));
    }
}
