﻿namespace ColegioMirim.WebApi.MVC.Models.Response
{
    public class ListarAlunosViewModel
    {
        public int Id { get; set; }
        public string RA { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public bool Ativo { get; set; }
        public DateTimeOffset CriadoEm { get; set; }
    }
}
