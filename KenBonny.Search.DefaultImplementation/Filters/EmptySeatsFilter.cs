using System.Collections.Generic;
using System.Linq;
using KenBonny.Search.DefaultImplementation.ReadModel;

namespace KenBonny.Search.DefaultImplementation.Filters
{
    public class EmptySeatsFilter : IFilter
    {
        public IEnumerable<ISeat> RemoveUnwantedSeats(IEnumerable<ISeat> availalbeSeats)
        {
            return availalbeSeats.Where(seat => seat.IsEmpty);
        }
    }
}