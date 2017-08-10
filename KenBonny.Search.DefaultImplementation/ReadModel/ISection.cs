using System.Collections.Generic;

namespace KenBonny.Search.DefaultImplementation.ReadModel
{
    public interface ISection
    {
        int Id { get; }
        IReadOnlyCollection<ITable> Tables { get; }
        IReadOnlyCollection<IServer> Servers { get; }
        IRestaurant Restaurant { get; }
    }
}