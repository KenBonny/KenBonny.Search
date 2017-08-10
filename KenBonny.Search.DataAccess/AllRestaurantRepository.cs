using System.Collections.Generic;
using System.Linq;
using KenBonny.Search.Core.Queries;
using KenBonny.Search.DataAccess.ReadModel;
using KenBonny.Search.DefaultImplementation;
using KenBonny.Search.DefaultImplementation.ReadModel;

namespace KenBonny.Search.DataAccess
{
    public class AllRestaurantRepository : IRestaurantRepository
    {
        public IList<IRestaurant> FindRestaurants(SearchQuery query)
        {
            return MockDatabase.Restaurants().ToList<IRestaurant>();
        }
    }
}