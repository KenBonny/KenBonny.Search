namespace KenBonny.Search.DefaultImplementation.ReadModel
{
    /// <summary>
    /// Person eating at a restaurant
    /// </summary>
    public class Diner
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Seat Seat { get; set; }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
}