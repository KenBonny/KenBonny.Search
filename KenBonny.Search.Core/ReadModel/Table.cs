using System.Collections.Generic;
using System.Linq;

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
        public bool IsEmpty => _seats.All(seat => seat.IsEmpty);

        public override string ToString()
        {
            var diners = string.Join(", ", Seats.Where(s => s.IsOccupied).Select(s => s.Diner));
            return $"Table {Id} (seated: {diners})";
        }
    }
}