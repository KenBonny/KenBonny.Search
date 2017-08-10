using System.Collections.Generic;

namespace KenBonny.Search.DefaultImplementation.ReadModel
{
    public interface IRestaurant
    {
        string Name { get; }
        IReadOnlyCollection<ISection> Sections { get; }
    }
}