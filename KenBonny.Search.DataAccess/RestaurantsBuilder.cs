using System.Collections.Generic;
using System.Linq;
using KenBonny.Search.Core.ReadModel;

namespace KenBonny.Search.DataAccess
{
    internal class RestaurantsBuilder
    {
        private List<Restaurant> _restaurants;

        private RestaurantsBuilder()
        {
            _restaurants = new List<Restaurant>();
        }

        internal static RestaurantsBuilder Init()
        {
            return new RestaurantsBuilder();
        }

        internal RestaurantsBuilder AddRestaurant(string name)
        {
            var restaurant = new Restaurant {Name = name};
            _restaurants.Add(restaurant);

            return this;
        }

        internal RestaurantsBuilder AddSection(string server)
        {
            var restaurant = _restaurants.Last();
            var section = new Section
            {
                Id = restaurant.Sections.Count,
                Servers = new[] {new Server {Name = server}}
            };

            restaurant.AddSection(section);

            return this;
        }

        internal RestaurantsBuilder AddTable(int seats)
        {
            var seatsList = new List<Seat>();
            for (var i = 0; i < seats; i++)
            {
                seatsList.Add(new Seat());
            }

            var table = new Table { Seats = seatsList};
            _restaurants.Last().Sections.Last().AddTable(table);

            return this;
        }

        internal IList<Restaurant> BuildRestaurants()
        {
            return _restaurants;
        }
    }
}