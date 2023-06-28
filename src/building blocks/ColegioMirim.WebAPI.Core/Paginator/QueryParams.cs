using ColegioMirim.WebAPI.Core.Swagger;
using System.Text.Json.Serialization;

namespace ColegioMirim.WebAPI.Core.Paginator
{
    public class QueryParams
    {
        public string OrderBy { get; set; }
        public OrderDirection? Direction { get; set; }
        public int? Page { get; set; }
        public int? PageSize { get; set; }
        public int? Offset => (Page - 1) * PageSize;
    }
}
