using System.Data;

namespace Genie.Core.Infrastructure.Interfaces
{
	/// <summary>
    /// A system wide context that holds the connection to the database and manages the connection
    /// </summary>
	public interface IDBContext
	{
	      /// <summary>
          /// Connection to the database
          /// </summary>
		  IDbConnection Connection { get; }
      
          /// <summary>
          /// Creates a new unit of work for this context
          /// </summary>
          /// <returns>A new unit of work</returns>
          IUnitOfWork Unit();
	}
}

