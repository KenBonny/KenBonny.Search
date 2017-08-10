using System.Collections.Generic;
using System.Linq;
using KenBonny.Search.DefaultImplementation.ReadModel;

namespace KenBonny.Search.DataAccess.ReadModel
{
    public class Table : ITable
    {
        private IReadOnlyCollection<Seat> _seats;
        public int Id { get; internal set; }

        public IReadOnlyCollection<ISeat> Seats
        {
            get => _seats;
            set
            {
                _seats = (IReadOnlyCollection<Seat>) value;
                foreach (var seat in _seats)
                {
                    seat.Table = this;
                }
            }
        }

        public ISection Section { get; internal set; }
        public bool IsEmpty => _seats.All(seat => seat.IsEmpty);

        public override string ToString()
        {
            var diners = string.Join(", ", Seats.Where(s => s.IsOccupied).Select(s => s.Diner));
            return $"Table {Id} (seated: {diners})";
        }
    }
}