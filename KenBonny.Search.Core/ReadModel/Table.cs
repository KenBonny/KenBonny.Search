using System.Collections.Generic;

namespace KenBonny.Search.Core.ReadModel
{
    public class Table
    {
        private IReadOnlyCollection<Seat> _seats;
        public int Id { get; set; }

        public IReadOnlyCollection<Seat> Seats
        {
            get => _seats;
            set
            {
                _seats = value;
                foreach (var seat in _seats)
                {
                    seat.Table = this;
                }
            }
        }

        public Section Section { get; set; }
    }
}