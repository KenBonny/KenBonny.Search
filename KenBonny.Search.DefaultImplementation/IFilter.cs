using System.Collections.Generic;
using System.Linq;
using KenBonny.Search.DefaultImplementation.ReadModel;

namespace KenBonny.Search.DefaultImplementation
{
    public interface IFilter
    {
        IEnumerable<ISeat> RemoveUnwantedSeats(IEnumerable<ISeat> availalbeSeats);
    }
}