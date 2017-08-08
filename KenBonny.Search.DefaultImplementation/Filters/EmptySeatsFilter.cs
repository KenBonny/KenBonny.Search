using System.Collections.Generic;
using System.Linq;
using KenBonny.Search.Core.ReadModel;

namespace KenBonny.Search.DefaultImplementation.Filters
{
    public class EmptySeatsFilter : IFilter
    {
        public IEnumerable<Seat> RemoveUnwantedSeats(IEnumerable<Seat> availalbeSeats)
        {
            return availalbeSeats.Where(seat => seat.IsEmpty);
        }
    }
}