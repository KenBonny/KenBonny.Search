namespace KenBonny.Search.Core.Queries
{
    public class UnreservedSeatInRestaurantQuery : SearchQuery
    {
        public UnreservedSeatInRestaurantQuery(string restaurant, SortOrder sortOrder = SortOrder.BestFirst) : base(sortOrder)
        {
            Restaurant = restaurant;
        }

        public string Restaurant { get; }
    }
}