using System.Linq;
using KenBonny.Search.Core.ReadModel;
using KenBonny.Search.DefaultImplementation.Decorators;

namespace KenBonny.Search.DataAccess
{
    public class ReservationRepository : IReservationRepository
    {
        public Seat FindReservedSeat(string firstName, string lastName)
        {
            var restaurants = MockDatabase.Restaurants();
            return restaurants
                .SelectMany(r => r.Sections)
                .SelectMany(section => section.Tables)
                .SelectMany(table => table.Seats)
                .Where(seat => seat.IsOccupied)
                .FirstOrDefault(seat => seat.Diner.FirstName.Equals(firstName) && seat.Diner.LastName.Equals(lastName));
        }
    }
}