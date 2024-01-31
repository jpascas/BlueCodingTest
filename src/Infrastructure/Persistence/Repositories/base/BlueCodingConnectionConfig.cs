using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistency.Repositories
{
    public class BlueCodingConnectionConfig : IDBConnectionConfig
    {
        public BlueCodingConnectionConfig(string connectionString)
        {
            ConnectionString = connectionString;
        }
        public string ConnectionString { get; }
    }    
}
