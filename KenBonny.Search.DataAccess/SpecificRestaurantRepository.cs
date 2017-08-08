using System;
using System.Collections.Generic;
using System.Linq;
using KenBonny.Search.Core.Queries;
using KenBonny.Search.Core.ReadModel;
using KenBonny.Search.DefaultImplementation;

namespace KenBonny.Search.DataAccess
{
    public class SpecificRestaurantRepository : IRestaurantRepository
    {
        public IList<Restaurant> FindRestaurants(SearchQuery query)
        {
            var unreservedSeatInRestaurantQuery = query as UnreservedSeatInRestaurantQuery;
            if (unreservedSeatInRestaurantQuery == null)
            {
                throw new NotSupportedException(
                    $"Cannot execute query based on insufficient data: passed {query.GetType().Name} instead of {nameof(UnreservedSeatInRestaurantQuery)}");
            }
            
            var restaurants = RestaurantsBuilder.Init()
                .AddRestaurant("Come Chez Moi")
                .AddSection("Ken")
                .AddTable(2)
                .AddTable(4)
                .AddTable(4)
                .AddSection("Kelly")
                .AddTable(4)
                .AddTable(8)
                .AddRestaurant("De Peirdestal")
                .AddSection("Joxer")
                .AddTable(2)
                .AddTable(2)
                .AddTable(4)
                .AddTable(4)
                .AddSection("Danny")
                .AddTable(6)
                .AddTable(6)
                .AddSection("Stijn")
                .AddTable(4)
                .AddTable(6)
                .AddTable(6)
                .BuildRestaurants();

            return restaurants.Where(r => r.Name.Equals(unreservedSeatInRestaurantQuery.Restaurant)).ToList();
        }
    }
}