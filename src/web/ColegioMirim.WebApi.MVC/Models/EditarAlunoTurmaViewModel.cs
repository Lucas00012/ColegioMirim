using ColegioMirim.Core.Communication;

namespace ColegioMirim.WebApi.MVC.Models
{
    public class EditarAlunoTurmaViewModel : ResponseResult
    {
        public int AlunoId { get; set; }
        public int TurmaId { get; set; }
        public bool Ativo { get; set; }
    }
}
