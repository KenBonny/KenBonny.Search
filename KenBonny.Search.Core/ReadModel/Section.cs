using System.Collections.Generic;

namespace KenBonny.Search.Core.ReadModel
{
    public class Section
    {
        private Restaurant _restaurant;
        private IReadOnlyCollection<Table> _tables;
        private IReadOnlyCollection<Server> _servers;

        public int Id { get; set; }
        
        public IReadOnlyCollection<Table> Tables
        {
            get => _tables;
            set
            {
                _tables = value;
                foreach (var table in _tables)
                {
                    table.Section = this;
                }
            }
        }

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
    }
}