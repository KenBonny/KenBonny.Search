using KenBonny.Search.DefaultImplementation.ReadModel;

namespace KenBonny.Search.DefaultImplementation.Decorators
{
    public interface IReservationRepository
    {
        ISeat FindReservedSeat(string firstName, string lastName);
    }
}