using System.Collections.Generic;
using KenBonny.Search.Core.Queries;
using KenBonny.Search.Core.ReturnModel;

namespace KenBonny.Search.Core
{
    public interface ISearcher
    {
        IReadOnlyCollection<Seat> FindSeats(SearchQuery query);
    }
}