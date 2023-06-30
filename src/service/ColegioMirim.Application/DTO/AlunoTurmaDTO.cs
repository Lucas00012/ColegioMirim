namespace ColegioMirim.Application.DTO
{
    public class AlunoTurmaDTO
    {
        public int Id { get; set; }
        public bool Ativo { get; set; }

        public int AlunoId { get; set; }
        public string AlunoNome { get; set; }
        public string AlunoRA { get; set; }

        public int TurmaId { get; set; }
        public string TurmaNome { get; set; }
        public int TurmaAno { get; set; }

        public DateTimeOffset VinculadoEm { get; set; }
    }
}
