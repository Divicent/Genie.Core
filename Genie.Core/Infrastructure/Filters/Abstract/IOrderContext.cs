using System.Collections.Generic;

namespace Genie.Core.Infrastructure.Filters.Abstract
{
    /// <summary>
    /// An order context is used to build the order by clause of the target query
    /// </summary>
	public interface IOrderContext
    {
        /// <summary>
        /// Adds an and condition
        /// </summary>
        void And();
        
        /// <summary>
        /// Adds a custom expression
        /// </summary>
        /// <param name="expression">Expression to apply</param>
        void Add(string expression);
        
        /// <summary>
        /// Current expressions as a Queue
        /// </summary>
        Queue<string> GetOrderExpressions();
    }
}


