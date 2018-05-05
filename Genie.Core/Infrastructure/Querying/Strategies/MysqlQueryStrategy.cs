using System.Linq;
using Genie.Core.Infrastructure.Filters.Abstract;

namespace Genie.Core.Infrastructure.Querying.Strategies
{
    public sealed class MysqlQueryStrategy : QueryStrategy
    {
        internal override string Enclose(string str) => $"`{str}`";

        internal override string Select(IRepoQuery query, bool isCount, QueryBuilder queryBuilder)
        {
            return string.Format("select {0} from " + query.Target, isCount ? "count(*)" : queryBuilder.CreateSelectColumnList(query.Columns.ToList(), query.Target));
        }

        internal override string Page(IRepoQuery query)
        {
            if (query.Page != null && query.PageSize != null)
            {
                return $" LIMIT {query.Page * query.PageSize}, {query.PageSize} ";
            }

            if (query.Skip != null || query.Take != null || query.Limit != null)
            {
                return $" LIMIT {query.Skip ?? 0},  {query.Take ?? query.Limit ?? 0} ";
            }

            return null;
        }

        internal override string GetId()
        {
            return "SELECT LAST_INSERT_ID()";
        }
    }
}
