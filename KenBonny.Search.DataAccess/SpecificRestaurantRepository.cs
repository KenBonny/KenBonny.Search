using System;
using System.Collections.Generic;
using System.Linq;
using KenBonny.Search.Core.Queries;
using KenBonny.Search.DataAccess.ReadModel;
using KenBonny.Search.DefaultImplementation;
using KenBonny.Search.DefaultImplementation.ReadModel;

namespace KenBonny.Search.DataAccess
{
    public class SpecificRestaurantRepository : IRestaurantRepository
    {
        public IList<IRestaurant> FindRestaurants(SearchQuery query)
        {
            var unreservedSeatInRestaurantQuery = query as UnreservedSeatInRestaurantQuery;
            if (unreservedSeatInRestaurantQuery == null)
            {
                throw new NotSupportedException(
                    $"Cannot execute query based on insufficient data: passed {query.GetType().Name} instead of {nameof(UnreservedSeatInRestaurantQuery)}");
            }
            
            return MockDatabase.Restaurants().Where(r => r.Name.Equals(unreservedSeatInRestaurantQuery.Restaurant)).ToList<IRestaurant>();
        }
    }
}