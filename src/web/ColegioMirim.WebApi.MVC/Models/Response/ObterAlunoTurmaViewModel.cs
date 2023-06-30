using ColegioMirim.Core.Communication;

namespace ColegioMirim.WebApi.MVC.Models.Response
{
    public class ObterAlunoTurmaViewModel : ResponseResult
    {
        public bool Ativo { get; set; }

        public int AlunoId { get; set; }
        public string AlunoNome { get; set; }

        public int TurmaId { get; set; }
        public string TurmaNome { get; set; }

        public DateTimeOffset VinculadoEm { get; set; }
    }
}
