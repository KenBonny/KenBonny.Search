using System.Collections.Generic;
using System.Linq;
using KenBonny.Search.Core;
using KenBonny.Search.Core.Queries;
using Seat = KenBonny.Search.Core.ReturnModel.Seat;

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
                var returnSeat = new Seat(reservedSeat.Table.Section.Restaurant.Name, 
                    reservedSeat.Table.Section.Id,
                    reservedSeat.Table.Id);
                return new[] {returnSeat};
            }

            return _innerSearcher.FindSeats(query);
        }
    }
}