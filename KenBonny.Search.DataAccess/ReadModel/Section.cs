using System.Collections.Generic;
using System.Linq;
using KenBonny.Search.DefaultImplementation.ReadModel;

namespace KenBonny.Search.DataAccess.ReadModel
{
    public class Section : ISection
    {
        private readonly List<Table> _tables = new List<Table>();

        private Restaurant _restaurant;
        private IReadOnlyCollection<Server> _servers;

        public int Id { get; set; }
        
        public IReadOnlyCollection<ITable> Tables => _tables;

        public IReadOnlyCollection<IServer> Servers
        {
            get => _servers;
            set
            {
                _servers = (IReadOnlyCollection<Server>) value;
                foreach (var server in _servers)
                {
                    server.AddSection(this);
                }
            }
        }

        public IRestaurant Restaurant { get; set; }

        internal void AddTable(Table table)
        {
            table.Section = this;
            _tables.Add(table);
        }

        public override string ToString()
        {
            var servers = string.Join(", ", Servers);
            var tables = string.Join(", ", Tables.Select(t => t.Id));
            return $"Section {Id} is served by {servers} for tables {tables}";
        }
    }
}