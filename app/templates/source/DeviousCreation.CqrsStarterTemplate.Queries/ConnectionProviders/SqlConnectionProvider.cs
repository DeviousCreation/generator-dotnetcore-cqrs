using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace DeviousCreation.CqrsStarterTemplate.Queries.ConnectionProviders
{
    public class SqlConnectionProvider : IDbConnectionProvider
    {
        private readonly string _connectionString;

        public SqlConnectionProvider(IConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            this._connectionString = configuration["ConnectionString"];
        }

        public IDbConnection Connection => new SqlConnection(this._connectionString);
    }
}