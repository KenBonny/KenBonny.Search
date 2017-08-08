using System.Collections.Generic;

namespace KenBonny.Search.Core.ReadModel
{
    public class Section
    {
        private Restaurant _restaurant;
        private List<Table> _tables;
        private IReadOnlyCollection<Server> _servers;

        public int Id { get; set; }
        
        public IReadOnlyCollection<Table> Tables => _tables;

        public IReadOnlyCollection<Server> Servers
        {
            get { return _servers; }
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
    }
}