using ColegioMirim.Core.Communication;

namespace ColegioMirim.WebApi.MVC.Models.Response
{
    public class Paginacao<T> : ResponseResult where T : class
    {
        public List<T> Items { get; set; }
        public int Page { get; set; }
        public int? PageSize { get; set; }
        public int TotalPages { get; set; }
        public int Count { get; set; }
    }
}
