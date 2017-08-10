using System.Collections.Generic;

namespace KenBonny.Search.DefaultImplementation.ReadModel
{
    public interface ITable
    {
        int Id { get; }
        IReadOnlyCollection<ISeat> Seats { get; }
        ISection Section { get; }
        bool IsEmpty { get; }
    }
}