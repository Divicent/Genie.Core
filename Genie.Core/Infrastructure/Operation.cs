using Genie.Core.Infrastructure.Interfaces;
using Genie.Core.Infrastructure.Models;

namespace Genie.Core.Infrastructure
{
    internal class Operation : IOperation
    {
        internal Operation(OperationType type, BaseModel model)
        {
            Type = type;
            Object = model;
        }

        public OperationType Type { get; }
        public BaseModel Object { get; }
    }
}

