using System.Collections.Generic;

namespace KenBonny.Search.Core.ReadModel
{
    public class Server
    {
        private List<Section> _sections = new List<Section>();
        
        public string Name { get; set; }

        public IReadOnlyCollection<Section> Sections => _sections;

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