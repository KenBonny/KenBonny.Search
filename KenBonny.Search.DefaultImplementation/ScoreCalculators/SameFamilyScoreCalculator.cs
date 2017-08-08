using System;
using System.Linq;
using KenBonny.Search.Core.Queries;
using KenBonny.Search.Core.ReadModel;

namespace KenBonny.Search.DefaultImplementation.ScoreCalculators
{
    public class SameFamilyScoreCalculator : IScoreCalculator
    {
        public int CalculateScore(Seat seat, SearchQuery query)
        {
            var unreservedSeatForDinerQuery = query as UnreservedSeatForDinerQuery;
            if (unreservedSeatForDinerQuery == null)
            {
                return 0;
            }

            if (seat.Table.Seats.Where(s => s.IsOccupied).Any(s => SameFamily(s, unreservedSeatForDinerQuery)))
            {
                return 1;
            }

            return -1;
        }

        private static bool SameFamily(Seat s, UnreservedSeatForDinerQuery query)
        {
            return s.Diner.LastName.Equals(query.DinerLastName, StringComparison.OrdinalIgnoreCase);
        }
    }
}