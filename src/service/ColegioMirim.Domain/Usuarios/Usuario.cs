using ColegioMirim.Core.DomainObjects;
using System.Security.Cryptography;
using System.Text;

namespace ColegioMirim.Domain.Usuarios
{
    public class Usuario : Entity, IAggregateRoot
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string SenhaHash { get; set; }
        public TipoUsuario TipoUsuario { get; set; }

        public static string GerarSenhaHash(string senha)
        {
            using var md5 = MD5.Create();
            var bytes = Encoding.UTF8.GetBytes(senha);
            var hash = md5.ComputeHash(bytes);

            return BitConverter.ToString(hash).Replace("-", "").ToLower();
        }
    }
}
