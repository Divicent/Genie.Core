using System;
using System.Data;
using System.Data.SqlClient;
using Genie.Core.Infrastructure.Interfaces;
using Genie.Core.Infrastructure.Querying;
using Genie.Core.Infrastructure.Querying.Strategies;
using MySql.Data.MySqlClient;

namespace Genie.Core.Infrastructure
{
	/// <summary>
    /// An Implementation that uses SqlConnection
    /// </summary>
	public class DBContext : IDBContext
    {
        private readonly Func<IDbConnection> _getConnection;


		/// <summary>
        /// Initialize  a new dapper context 
        /// </summary>
        public DBContext(IDatabaseMetadataProvider databaseMetadataProvider)
		{
		    var connectionString = databaseMetadataProvider.GetConnectionString();

		    switch (databaseMetadataProvider.GetDbms())
            {
                case Dbms.Mssql:
                    QueryStrategy = new MicrosoftSqlServerQueryStrategy();
                    _getConnection = () => new SqlConnection(connectionString);
                    break;
                case Dbms.Mysql:
                    QueryStrategy = new MysqlQueryStrategy();
                   _getConnection = () => new MySqlConnection(connectionString);
                    break;
            }
		}

        public IUnitOfWork Unit() 
        {
            return new UnitOfWork(this);
        }

        public QueryStrategy QueryStrategy { get; }

        public IDbConnection GetConnection()
        {
            return _getConnection();
        }
    }
}

