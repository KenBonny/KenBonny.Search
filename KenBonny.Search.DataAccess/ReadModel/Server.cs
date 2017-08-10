using System.Collections.Generic;
using KenBonny.Search.DefaultImplementation.ReadModel;

namespace KenBonny.Search.DataAccess.ReadModel
{
    public class Server : IServer
    {
        private List<Section> _sections = new List<Section>();
        
        public string Name { get; set; }

        public IReadOnlyCollection<ISection> Sections => _sections;

        internal void AddSection(Section section)
        {
            _sections.Add(section);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}