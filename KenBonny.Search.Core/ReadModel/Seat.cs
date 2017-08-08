namespace KenBonny.Search.Core.ReadModel
{
    public class Seat
    {
        private Diner _diner;

        public Diner Diner
        {
            get { return _diner; }
            set
            {
                _diner = value;
                _diner.Seat = this;
            }
        }

        public Table Table { get; set; }
        public bool IsEmpty => Diner == null;
        public bool IsOccupied => Diner != null;
    }
}