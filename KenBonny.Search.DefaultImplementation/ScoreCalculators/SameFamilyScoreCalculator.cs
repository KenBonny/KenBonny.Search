using System;
using System.Linq;
using KenBonny.Search.Core.Queries;
using KenBonny.Search.DefaultImplementation.ReadModel;

namespace KenBonny.Search.DefaultImplementation.ScoreCalculators
{
    public class SameFamilyScoreCalculator : IScoreCalculator
    {
        private const int NoScore = 0;
        private const int TableWithFamily = 1;
        private const int EmptyTable = 2;
        private const int TableWithStrangers = 3;
        
        public int CalculateScore(ISeat seat, SearchQuery query)
        {
            var unreservedSeatForDinerQuery = query as UnreservedSeatForDinerQuery;
            if (unreservedSeatForDinerQuery == null)
            {
                return NoScore;
            }

            if (seat.Table.Seats.Where(s => s.IsOccupied).Any(s => SameFamily(s, unreservedSeatForDinerQuery)))
            {
                return TableWithFamily;
            }
            
            if (seat.Table.IsEmpty)
            {
                return EmptyTable;
            }

            return TableWithStrangers;
        }

        private static bool SameFamily(ISeat s, UnreservedSeatForDinerQuery query)
        {
            return s.Diner.LastName.Equals(query.DinerLastName, StringComparison.OrdinalIgnoreCase);
        }
    }
}