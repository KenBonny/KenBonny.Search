using System.Linq;
using KenBonny.Search.Core.ReadModel;

namespace KenBonny.Search.DefaultImplementation.Sorters
{
    public class PreferTablesWithGuestsSorter : ISorter
    {
        private const int Equal = 0;
        private const int PreferLeft = 1;
        private const int PreferRight = -1;
        
        public int Compare(Seat left, Seat right)
        {
            if (IsNull(left) && IsNull(right))
            {
                return Equal;
            }

            if (IsNull(left) && IsNotNull(right))
            {
                return PreferRight;
            }

            if (IsNotNull(left) && IsNull(right))
            {
                return PreferLeft;
            }

            // ReSharper disable PossibleNullReferenceException
            return right.Table.Seats.Count(s => s.IsOccupied) - left.Table.Seats.Count(s => s.IsOccupied);
            // ReSharper restore PossibleNullReferenceException
        }

        private static bool IsNotNull(Seat seat)
        {
            return seat != null;
        }

        private static bool IsNull(Seat seat)
        {
            return seat == null;
        }
    }
}