using KenBonny.Search.Core.ReadModel;

namespace KenBonny.Search.Core.Queries
{
    public class UnreservedSeatForDinerQuery : SearchQuery
    {
        public UnreservedSeatForDinerQuery(Diner diner, SortOrder sortOrder = SortOrder.BestFirst) : base(sortOrder)
        {
            Diner = diner;
        }

        public Diner Diner { get; }
    }
}