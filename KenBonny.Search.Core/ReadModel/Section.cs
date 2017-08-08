using System.Collections.Generic;
using System.Linq;

namespace KenBonny.Search.Core.ReadModel
{
    public class Section
    {
        private readonly List<Table> _tables = new List<Table>();

        private Restaurant _restaurant;
        private IReadOnlyCollection<Server> _servers;

        public int Id { get; set; }
        
        public IReadOnlyCollection<Table> Tables => _tables;

        public IReadOnlyCollection<Server> Servers
        {
            get => _servers;
            set
            {
                _servers = value;
                foreach (var server in _servers)
                {
                    server.AddSection(this);
                }
            }
        }

        public Restaurant Restaurant { get; set; }

        public void AddTable(Table table)
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