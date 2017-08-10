namespace KenBonny.Search.DefaultImplementation.ReadModel
{
    public class Seat
    {
        private Diner _diner;

        public Diner Diner
        {
            get => _diner;
            set
            {
                _diner = value;
                _diner.Seat = this;
            }
        }

        public Table Table { get; set; }
        public bool IsEmpty => Diner == null;
        public bool IsOccupied => Diner != null;

        public override string ToString()
        {
            var diner = Diner?.ToString() ?? "Nobody";
            return $"Placed at {Table}, here sits {diner}";
        }
    }
}