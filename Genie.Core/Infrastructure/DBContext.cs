using System.Data;
using System.Data.SqlClient;
using Genie.Core.Infrastructure.Interfaces;

namespace Genie.Core.Infrastructure
{
	/// <summary>
    /// An Implementation that uses SqlConnection
    /// </summary>
	public class DBContext : IDBContext
    {
        private readonly string _connectionString;
        private IDbConnection _connection;

		/// <summary>
        /// Initialize  a new dapper context 
        /// </summary>
        public DBContext(IConnectionStringProvider connectionStringProvider)
        {
            _connectionString = connectionStringProvider.GetConnectionString();
        }

    	/// <summary>
        /// Get the connection to the database
        /// </summary>
        public IDbConnection Connection => _connection ?? (_connection = new SqlConnection(_connectionString));

        public IUnitOfWork Unit() 
        {
            return new UnitOfWork(this);
        }
    }
}

