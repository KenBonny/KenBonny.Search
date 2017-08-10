using System.Collections.Generic;
using KenBonny.Search.Core.Queries;
using KenBonny.Search.DefaultImplementation;
using KenBonny.Search.DefaultImplementation.ReadModel;

namespace KenBonny.Search.DataAccess
{
    public class AllRestaurantRepository : IRestaurantRepository
    {
        public IList<Restaurant> FindRestaurants(SearchQuery query)
        {
            return MockDatabase.Restaurants();
        }
    }
}