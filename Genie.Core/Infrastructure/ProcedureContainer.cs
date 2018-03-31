using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Genie.Core.Infrastructure.Interfaces;
using Genie.Core.Mapper;

namespace Genie.Core.Infrastructure
{
	public class ProcedureContainer: IProcedureContainer
    {
		private IDBContext Context { get; }

		internal ProcedureContainer(IDBContext context)
		{
		    Context = context;
		}

		private SqlConnection GetConnection()
		{
			return new SqlConnection(Context.Connection.ConnectionString);
		}

		private void Execute(string  name, object parameters) 
		{
			using(var connection = GetConnection())
			{
				connection.Open();
				connection.Execute(name, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		private T QuerySingle<T>(string  name, object parameters) 
		{
			using(var connection = GetConnection())
			{
				connection.Open();
				return connection.QueryFirstOrDefault<T>(name, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		private IEnumerable<T> QueryList<T>(string  name, object parameters) 
		{
			using(var connection = GetConnection())
			{
				connection.Open();
				return connection.Query<T>(name, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		private async Task ExecuteAsync(string  name, object parameters) 
		{
			using(var connection = GetConnection())
			{
				connection.Open();
				await connection.ExecuteAsync(name, parameters, commandType: CommandType.StoredProcedure);
				connection.Close();
			}
		}

		private async Task<T> QuerySingleAsync<T>(string  name, object parameters) 
		{
			using(var connection = GetConnection())
			{
				connection.Open();
				return await connection.QueryFirstOrDefaultAsync<T>(name, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		private async Task<IEnumerable<T>> QueryListAsync<T>(string  name, object parameters) 
		{
			using(var connection = GetConnection())
			{
				connection.Open();
				return await connection.QueryAsync<T>(name, parameters, commandType: CommandType.StoredProcedure);
			}
		}
    }
}

