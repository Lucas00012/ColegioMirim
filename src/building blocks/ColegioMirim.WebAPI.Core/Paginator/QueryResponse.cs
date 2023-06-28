namespace ColegioMirim.WebAPI.Core.Paginator
{
    public class QueryResponse<T> where T : class
    {
        public List<T> Items { get; set; }
        public int Page { get; set; }
        public int? PageSize { get; set; }
        public int TotalPages => PageSize == 0 ? 0 : (int)Math.Ceiling(Count / (decimal)PageSize);
        public int Count { get; set; }
    }
}
