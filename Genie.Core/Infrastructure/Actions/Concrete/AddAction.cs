
using System;
using Genie.Core.Infrastructure.Actions.Abstract;

namespace Genie.Core.Infrastructure.Actions.Concrete
{
    internal class AddAction: IAddAction
    {
        private readonly Action<object> _action;
        private readonly object _parameter;

        internal AddAction(Action<object> action, object parameter )
        {
            _action = action;
            _parameter = parameter;
        }

        public void Run()
        {
            if(_action == null || _parameter ==null)
            { return; }
            _action(_parameter);
        }
    }
}
