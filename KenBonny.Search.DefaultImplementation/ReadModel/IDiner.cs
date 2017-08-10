namespace KenBonny.Search.DefaultImplementation.ReadModel
{
    public interface IDiner
    {
        string FirstName { get; }
        
        string LastName { get; }
        
        ISeat Seat { get; }
    }
}