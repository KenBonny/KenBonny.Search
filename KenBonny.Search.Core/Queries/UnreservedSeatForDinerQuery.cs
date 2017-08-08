namespace KenBonny.Search.Core.Queries
{
    public class UnreservedSeatForDinerQuery : SearchQuery
    {
        public UnreservedSeatForDinerQuery(string dinerFirstName, string dinerLastName, SortOrder sortOrder = SortOrder.BestFirst) : base(sortOrder)
        {
            DinerFirstName = dinerFirstName;
            DinerLastName = dinerLastName;
        }

        public string DinerFirstName { get; }
        
        public string DinerLastName { get; }
    }
}