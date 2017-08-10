using System.Collections.Generic;

namespace KenBonny.Search.DefaultImplementation.ReadModel
{
    public interface IServer
    {
        string Name { get; }
        IReadOnlyCollection<ISection> Sections { get; }
    }
}