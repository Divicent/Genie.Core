using System;
using System.Linq;
using Genie.Core.Infrastructure.Filters.Abstract;

namespace Genie.Core.Infrastructure.Filters.Concrete
{
    public class TimeSpanFilter<T, TQ> : ITimeSpanFilter<T, TQ> where T : IFilterContext
    {
        private readonly string _propertyName;
        private readonly T _parent;
        private readonly TQ _q;

        public TimeSpanFilter(string propertyName, T parent, TQ q)
        {
            _parent = parent;
            _propertyName = propertyName;
            _q = q;
        }

        public IExpressionJoin<T, TQ> EqualsTo(TimeSpan time)
        {
            _parent.Add(QueryMaker.EqualsTo(_propertyName, time, true));
            return new ExpressionJoin<T, TQ>(_parent, _q);
        }

        public IExpressionJoin<T, TQ> NotEquals(TimeSpan time)
        {
            _parent.Add(QueryMaker.NotEquals(_propertyName, time, true));
            return new ExpressionJoin<T, TQ>(_parent, _q);
        }

        public IExpressionJoin<T, TQ> LargerThan(TimeSpan time)
        {
            _parent.Add(QueryMaker.GreaterThan(_propertyName, time, true));
            return new ExpressionJoin<T, TQ>(_parent, _q);
        }

        public IExpressionJoin<T, TQ> LessThan(TimeSpan time)
        {
            _parent.Add(QueryMaker.LessThan(_propertyName, time, true));
            return new ExpressionJoin<T, TQ>(_parent, _q);
        }

        public IExpressionJoin<T, TQ> LargerThanOrEqualTo(TimeSpan time)
        {
            _parent.Add(QueryMaker.GreaterThanOrEquals(_propertyName, time, true));
            return new ExpressionJoin<T, TQ>(_parent, _q);
        }

        public IExpressionJoin<T, TQ> LessThanOrEqualTo(TimeSpan time)
        {
            _parent.Add(QueryMaker.LessThanOrEquals(_propertyName, time, true));
            return new ExpressionJoin<T, TQ>(_parent, _q);
        }

        public IExpressionJoin<T, TQ> Between(TimeSpan from, TimeSpan to)
        {
            _parent.Add(QueryMaker.Between(_propertyName, from, to, true));
            return new ExpressionJoin<T, TQ>(_parent, _q);
        }

        public IExpressionJoin<T, TQ> IsNull()
        {
            _parent.Add(QueryMaker.IsNull(_propertyName));
            return new ExpressionJoin<T, TQ>(_parent, _q);
        }

        public IExpressionJoin<T, TQ> IsNotNull()
        {
            _parent.Add(QueryMaker.IsNotNull(_propertyName));
            return new ExpressionJoin<T, TQ>(_parent, _q);
        }

        public IExpressionJoin<T, TQ> IsNull(bool isNull)
        {
            _parent.Add(isNull ? QueryMaker.IsNull(_propertyName) : QueryMaker.IsNotNull(_propertyName));
            return new ExpressionJoin<T, TQ>(_parent, _q);
        }

        public IExpressionJoin<T, TQ> In(params TimeSpan[] items)
        {
            _parent.Add(QueryMaker.In(_propertyName, items.Cast<object>().ToArray(), true));
            return new ExpressionJoin<T, TQ>(_parent, _q);
        }

        public IExpressionJoin<T, TQ> NotIn(params TimeSpan[] items)
        {
            _parent.Add(QueryMaker.NotIn(_propertyName, items.Cast<object>().ToArray(), true));
            return new ExpressionJoin<T, TQ>(_parent, _q);
        }
    }
}
