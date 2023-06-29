namespace ColegioMirim.WebApi.MVC.Models.Response
{
    public class ListarTurmasViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }
        public int Ano { get; set; }
        public DateTimeOffset CriadoEm { get; set; }
    }
}
