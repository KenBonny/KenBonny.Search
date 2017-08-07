using System.Collections.Generic;
using KenBonny.Search.Core.Queries;
using KenBonny.Search.Core.ReadModel;

namespace KenBonny.Search.DefaultImplementation
{
    public interface IRestaurantRepository
    {
        IList<Restaurant> FindRestaurants(SearchQuery query);
    }
}