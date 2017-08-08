using System.Collections.Generic;
using System.Linq;
using KenBonny.Search.Core.ReadModel;

namespace KenBonny.Search.DefaultImplementation
{
    public interface IFilter
    {
        IEnumerable<Seat> RemoveUnwantedSeats(IEnumerable<Seat> availalbeSeats);
    }
}