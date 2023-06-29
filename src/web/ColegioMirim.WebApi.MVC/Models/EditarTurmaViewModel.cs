using FluentMigrator.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace ColegioMirim.WebApi.MVC.Models
{
    public class EditarTurmaViewModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Nome { get; set; }

        public bool Ativo { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public int Ano { get; set; }
    }
}
