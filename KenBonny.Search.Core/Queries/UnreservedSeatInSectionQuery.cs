using KenBonny.Search.Core.ReadModel;

namespace KenBonny.Search.Core.Queries
{
    public class UnreservedSeatInSectionQuery : SearchQuery
    {
        public UnreservedSeatInSectionQuery(Section section, SortOrder sortOrder = SortOrder.BestFirst) : base(sortOrder)
        {
            Section = section;
        }

        public Section Section { get; }
    }
}