using ColegioMirim.Core.Communication;

namespace ColegioMirim.WebApi.MVC.Models.Response
{
    public class PaginacaoViewModel<T> : ResponseResult, IPaginacao where T : class
    {
        public List<T> Items { get; set; }
        public int Page { get; set; }
        public int? PageSize { get; set; }
        public int TotalPages { get; set; }
        public int Count { get; set; }
    }

    public interface IPaginacao
    {
        int Page { get; set; }
        int? PageSize { get; set; }
        int TotalPages { get; set; }
        int Count { get; set; }
    }
}
