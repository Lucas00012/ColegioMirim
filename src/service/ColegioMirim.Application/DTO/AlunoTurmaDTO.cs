namespace ColegioMirim.Application.DTO
{
    public class AlunoTurmaDTO
    {
        public bool Ativo { get; set; }

        public int AlunoId { get; set; }
        public string AlunoNome { get; set; }

        public int TurmaId { get; set; }
        public string TurmaNome { get; set; }

        public DateTimeOffset VinculadoEm { get; set; }
    }
}
