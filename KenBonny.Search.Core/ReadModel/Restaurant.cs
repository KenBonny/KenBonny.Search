using System.Collections.Generic;

namespace KenBonny.Search.Core.ReadModel
{
    public class Restaurant
    {
        private readonly List<Section> _sections = new List<Section>();
        public string Name { get; set; }

        public IReadOnlyCollection<Section> Sections => _sections;

        public void AddSection(Section section)
        {
            section.Restaurant = this;
            _sections.Add(section);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}