using FluentMigrator.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace ColegioMirim.WebApi.MVC.Models
{
    public class RegistrarTurmaViewModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public int Ano { get; set; }
    }
}
