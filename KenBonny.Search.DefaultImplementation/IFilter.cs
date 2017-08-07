using System.Collections.Generic;
using KenBonny.Search.Core.ReadModel;

namespace KenBonny.Search.DefaultImplementation
{
    public interface IFilter
    {
        IList<Seat> RemoveUnwantedSeats(IList<Seat> availalbeSeats);
    }
}