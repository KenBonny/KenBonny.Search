using System.Collections.Generic;
using System.Linq;
using KenBonny.Search.DataAccess.ReadModel;
using KenBonny.Search.DefaultImplementation.ReadModel;

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

            var section = (Section) _restaurants.Last().Sections.Last();
            var table = new Table
            {
                Id = section.Tables.Count,
                Seats = seatsList
            };
            section.AddTable(table);

            return this;
        }

        internal RestaurantsBuilder AddDiner(string firstName, string lastName)
        {
            var diner = new Diner{ FirstName = firstName, LastName = lastName };
            var seat = (Seat) _restaurants.Last().Sections.Last().Tables.Last().Seats.First(s => s.IsEmpty);
            seat.Diner = diner;

            return this;
        }

        internal IList<Restaurant> BuildRestaurants()
        {
            return _restaurants;
        }
    }
}