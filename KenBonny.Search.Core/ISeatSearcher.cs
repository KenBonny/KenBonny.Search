using System.Collections.Generic;
using KenBonny.Search.Core.Queries;
using KenBonny.Search.Core.ReturnModel;

namespace KenBonny.Search.Core
{
    public interface ISeatSearcher
    {
        IReadOnlyCollection<Seat> FindSeats(SearchQuery query);
    }
}