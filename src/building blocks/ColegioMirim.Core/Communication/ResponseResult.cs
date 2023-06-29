namespace ColegioMirim.Core.Communication
{
    public class ResponseResult
    {
        public string Title { get; set; }
        public int Status { get; set; }
        public ResponseErrorResult Errors { get; set; } = new();

        public bool Failed => Errors.Mensagens.Any();
    }
}
