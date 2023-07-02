using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ColegioMirim.WebApi.MVC.Models
{
    public class AlterarSenhaViewModel
    {
        [DisplayName("Senha atual")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string SenhaAtual { get; set; }

        [DisplayName("Nova senha")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 8)]
        public string NovaSenha { get; set; }

        [DisplayName("Confirme sua senha")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Compare(nameof(NovaSenha), ErrorMessage = "As senhas não conferem")]
        public string ConfirmarNovaSenha { get; set; }
    }
}
