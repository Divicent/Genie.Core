using System;
using System.Data;
using System.Threading.Tasks;
using System.Linq;
using Genie.Core.Infrastructure.Models;
using Genie.Core.Mapper;
using QB = Genie.Core.Infrastructure.Querying.QueryBuilder;

namespace Genie.Core.Extensions
{
    public static class MapperExtensions
    {
        /// <summary>
        /// Inserts an entity into table "T" and returns identity id.
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="entityToInsert">Entity to insert</param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>Identity of inserted entity</returns>
        public static long? Insert(this IDbConnection connection, BaseModel entityToInsert, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            using (connection)
            {
                connection.Open();
                var cmd = QB.Insert(entityToInsert);
                connection.Execute(cmd, entityToInsert, transaction, commandTimeout);
                var r = connection.Query(QB.GetId(), transaction: transaction, commandTimeout: commandTimeout).ToList();
                long id = 0;
                if (r.Any())
                {
                    try
                    {
                        id = (long)r.First().id;
                    }
                    catch (Exception)
                    { /*Ignored*/ }
                }
                return id;
            }
        }



        /// <summary>
        /// Inserts an entity into table "T" and returns identity id asynchronously.
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="entityToInsert">Entity to insert</param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>Identity of inserted entity</returns>
        public static async Task<long?> InsertAsync(this IDbConnection connection, BaseModel entityToInsert, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            using (connection)
            {
                connection.Open();
                var cmd = QB.Insert(entityToInsert);
                await connection.ExecuteAsync(cmd, entityToInsert, transaction, commandTimeout);
                var r = (await connection.QueryAsync(QB.GetId(), transaction: transaction, commandTimeout: commandTimeout)).ToList();
                long id = 0;
                if (r.Any())
                {
                    try
                    {
                        id = (long)r.First().id;
                    }
                    catch (Exception)
                    { /*Ignored*/ }
                }
                return id;
            }
        }

        /// <summary>
        /// Updates entity in table "Ts", checks if the entity is modified if the entity is tracked by the Get() extension.
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="entityToUpdate">Entity to be updated</param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>true if updated, false if not found or not modified (tracked entities)</returns>
        public static bool Update(this IDbConnection connection, BaseModel entityToUpdate, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var query = QB.Update(entityToUpdate);
            if (query == null)
            {
                return false;
            }
            using (connection)
            {
                connection.Open();
                var updated = connection.Execute(query, entityToUpdate, commandTimeout: commandTimeout, transaction: transaction);
                return updated > 0;
            }
        }

        /// <summary>
        /// Updates entity in table "Ts", checks if the entity is modified if the entity is tracked by the Get() extension asynchronously.
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="entityToUpdate">Entity to be updated</param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>true if updated, false if not found or not modified (tracked entities)</returns>
        public static async Task<bool> UpdateAsync(this IDbConnection connection, BaseModel entityToUpdate, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var query = QB.Update(entityToUpdate);
            if (query == null)
            {
                return false;
            }
            using (connection)
            {
                connection.Open();
                var updated = await connection.ExecuteAsync(query, entityToUpdate, commandTimeout: commandTimeout, transaction: transaction);
                return updated > 0;
            }
        }

        /// <summary>
        /// Delete entity in table "Ts".
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="entity"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>true if deleted, false if not found</returns>
        public static bool Delete(this IDbConnection connection, BaseModel entity, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            using (connection)
            {
                connection.Open();
                var deleted = connection.Execute(QB.Delete(entity), entity, transaction: transaction, commandTimeout: commandTimeout) > 0;
                return deleted;
            }
        }


        /// <summary>
	    /// Delete entity in table "Ts" asynchronously.
	    /// </summary>
	    /// <param name="connection">Open SqlConnection</param>
	    /// <param name="entity"></param>
	    /// <param name="transaction"></param>
	    /// <param name="commandTimeout"></param>
	    /// <returns>true if deleted, false if not found</returns>
	   	public static async Task<bool> DeleteAsync(this IDbConnection connection, BaseModel entity, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            using (connection)
            {
                connection.Open();
                var deleted = await connection.ExecuteAsync(QB.Delete(entity), entity, transaction: transaction, commandTimeout: commandTimeout) > 0;
                return deleted;
            }
        }
    }
}
