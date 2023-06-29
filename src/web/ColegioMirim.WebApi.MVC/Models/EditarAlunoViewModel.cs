using System.ComponentModel.DataAnnotations;

namespace ColegioMirim.WebApi.MVC.Models
{
    public class EditarAlunoViewModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string RA { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [EmailAddress(ErrorMessage = "O campo {0} está em formato inválido")]
        public string Email { get; set; }

        public bool Ativo { get; set; }
    }
}
