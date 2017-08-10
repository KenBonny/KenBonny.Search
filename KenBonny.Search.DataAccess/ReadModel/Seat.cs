using KenBonny.Search.DefaultImplementation.ReadModel;

namespace KenBonny.Search.DataAccess.ReadModel
{
    public class Seat : ISeat
    {
        private Diner _diner;

        public IDiner Diner
        {
            get => _diner;
            internal set
            {
                _diner = (Diner) value;
                _diner.Seat = this;
            }
        }

        public ITable Table { get; internal set; }
        
        public bool IsEmpty => Diner == null;
        public bool IsOccupied => Diner != null;

        public override string ToString()
        {
            var diner = Diner?.ToString() ?? "Nobody";
            return $"Placed at {Table}, here sits {diner}";
        }
    }
}