using KenBonny.Search.DefaultImplementation.ReadModel;

namespace KenBonny.Search.DataAccess.ReadModel
{
    /// <summary>
    /// Person eating at a restaurant
    /// </summary>
    public class Diner : IDiner
    {
        public string FirstName { get; internal set; }

        public string LastName { get; internal set; }

        public ISeat Seat { get; internal set; }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
}