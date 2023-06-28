namespace ColegioMirim.WebAPI.Core.Paginator
{
    public class OrderByOption
    {
        public OrderByOption(string field, string alias = null, string column = null)
        {
            Field = field;
            Alias = alias;
            Column = column ?? field;
        }

        public string Field { get; set; }
        public string Column { get; set; }
        public string Alias { get; set; }
    }
}
