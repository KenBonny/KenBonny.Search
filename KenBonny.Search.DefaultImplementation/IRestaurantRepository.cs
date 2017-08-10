using System.Collections.Generic;
using KenBonny.Search.Core.Queries;
using KenBonny.Search.DefaultImplementation.ReadModel;

namespace KenBonny.Search.DefaultImplementation
{
    public interface IRestaurantRepository
    {
        IList<IRestaurant> FindRestaurants(SearchQuery query);
    }
}