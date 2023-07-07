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

        public string Field { get; private set; }
        public string Column { get; private set; }
        public string Alias { get; private set; }

        public string Order => $"{(string.IsNullOrEmpty(Alias) ? "" : $"{Alias}.")}{Column}";

        public bool Equals(string identifier)
        {
            return Field.Equals(identifier, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
