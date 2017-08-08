using KenBonny.Search.Core.ReadModel;

namespace KenBonny.Search.DefaultImplementation.Decorators
{
    public interface IReservationRepository
    {
        Seat FindReservedSeat(string firstName, string lastName);
    }
}