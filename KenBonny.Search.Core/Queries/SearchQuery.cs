namespace KenBonny.Search.Core.Queries
{
    public class SearchQuery
    {
        public SearchQuery(SortOrder sortOrder)
        {
            SortOrder = sortOrder;
        }

        public SortOrder SortOrder { get; }
    }
}