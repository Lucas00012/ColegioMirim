using FluentMigrator.Infrastructure;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ColegioMirim.WebApi.MVC.Models
{
    public class RegistrarAlunoViewModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string RA { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [EmailAddress(ErrorMessage = "O campo {0} está em formato inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 8)]
        public string Senha { get; set; }

        [DisplayName("Confirme sua senha")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Compare(nameof(Senha), ErrorMessage = "As senhas não conferem")]
        public string ConfirmarSenha { get; set; }
    }
}
