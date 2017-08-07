namespace KenBonny.Search.Core.ReadModel
{
    public class Seat
    {
        private Diner _diner;
        public int Id { get; set; }

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
    }
}