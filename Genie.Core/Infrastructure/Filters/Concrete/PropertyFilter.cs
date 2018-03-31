using Genie.Core.Infrastructure.Filters.Abstract;

namespace Genie.Core.Infrastructure.Filters.Concrete
{
    internal class PropertyFilter : IPropertyFilter
    {
        public string PropertyName { get; set; }
        public string Type { get; set; }
        public object Value { get; set; }
    }
}

