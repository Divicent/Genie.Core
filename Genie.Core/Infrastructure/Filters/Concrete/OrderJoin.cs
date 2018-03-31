using Genie.Core.Infrastructure.Filters.Abstract;

namespace Genie.Core.Infrastructure.Filters.Concrete
{
    public class OrderJoin<T, TQ> : IOrderJoin<T, TQ> where T : IOrderContext
    {
        private readonly T _t;
        private readonly TQ _q;

        internal OrderJoin(T t, TQ q)
        {
            _t = t;
            _q = q;
        }

        public T And { get { _t.And(); return _t; } }

        public TQ Order()
        {
            return _q;
        }
    }
}

