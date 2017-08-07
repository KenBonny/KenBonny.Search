using System.Collections.Generic;

namespace KenBonny.Search.Core.ReadModel
{
    public class Restaurant
    {
        private IReadOnlyCollection<Section> _section;
        public string Name { get; set; }

        public IReadOnlyCollection<Section> Section
        {
            get => _section;
            set
            {
                _section = value;
                foreach (var section in _section)
                {
                    section.Restaurant = this;
                }
            }
        }
    }
}