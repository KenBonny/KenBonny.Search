namespace KenBonny.Search.DefaultImplementation.ReadModel
{
    public interface ISeat
    {
        IDiner Diner { get; }
        
        ITable Table { get; }
        
        bool IsEmpty { get; }
        
        bool IsOccupied { get; }
    }
}