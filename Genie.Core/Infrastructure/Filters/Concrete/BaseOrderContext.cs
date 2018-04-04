using System.Collections.Generic;
using Genie.Core.Infrastructure.Filters.Abstract;

namespace Genie.Core.Infrastructure.Filters.Concrete
{
    public abstract class BaseOrderContext : IOrderContext
    {
        protected BaseOrderContext() { Expressions = new Queue<OrderExpression>(); }
        protected Queue<OrderExpression> Expressions { get; set; }
        public void Add(OrderExpression expression) { Expressions.Enqueue(expression); }
        public Queue<OrderExpression> GetOrderExpressions() { return Expressions; }
    }
}

