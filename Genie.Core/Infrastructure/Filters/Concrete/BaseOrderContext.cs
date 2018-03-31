using System.Collections.Generic;
using Genie.Core.Infrastructure.Filters.Abstract;

namespace Genie.Core.Infrastructure.Filters.Concrete
{
    public abstract class BaseOrderContext : IOrderContext
    {
        protected BaseOrderContext() { Expressions = new Queue<string>(); }
        protected Queue<string> Expressions { get; set; }
        public void And() { Expressions.Enqueue(","); }
        public void Add(string expression) { Expressions.Enqueue(expression); }
        public Queue<string> GetOrderExpressions() { return Expressions; }
    }
}

