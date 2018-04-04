using Genie.Core.Infrastructure.Filters.Abstract;

namespace Genie.Core.Infrastructure.Querying
{
    public abstract class QueryStrategy
    {
        internal abstract string Enclose(string str);
        internal abstract string Select(IRepoQuery query, bool isCount, QueryBuilder queryBuilder);
        internal abstract string Page(IRepoQuery query);
    }
}
