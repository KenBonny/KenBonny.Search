using System.Collections.Generic;
using KenBonny.Search.Core;
using KenBonny.Search.Core.Queries;
using KenBonny.Search.Core.ReadModel;

namespace KenBonny.Search.DefaultImplementation.Decorators
{
    public class ReservationCheckerDecorator : ISearcher
    {
        private readonly ISearcher _innerSearcher;
        private readonly IReservationRepository _reservationRepository;

        public ReservationCheckerDecorator(ISearcher innerSearcher, IReservationRepository reservationRepository)
        {
            _innerSearcher = innerSearcher;
            _reservationRepository = reservationRepository;
        }

        public IReadOnlyCollection<Seat> FindSeats(SearchQuery query)
        {
            var unreservedSeatForDinerQuery = query as UnreservedSeatForDinerQuery;
            if (unreservedSeatForDinerQuery == null)
            {
                return _innerSearcher.FindSeats(query);
            }

            var reservedSeat = _reservationRepository.FindReservedSeat(unreservedSeatForDinerQuery.DinerFirstName, unreservedSeatForDinerQuery.DinerLastName);
            if (reservedSeat != null)
            {
                return new[] {reservedSeat};
            }

            return _innerSearcher.FindSeats(query);
        }
    }
}