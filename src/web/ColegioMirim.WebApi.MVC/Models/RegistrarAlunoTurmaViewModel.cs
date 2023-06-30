using ColegioMirim.Core.Communication;
using FluentMigrator.Infrastructure;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ColegioMirim.WebApi.MVC.Models
{
    public class RegistrarAlunoTurmaViewModel
    {
        [DisplayName("Aluno")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public int AlunoId { get; set; }

        [DisplayName("Turma")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public int TurmaId { get; set; }
    }
}
