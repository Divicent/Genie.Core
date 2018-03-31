using System.Collections.Generic;
using System.Data;

namespace Genie.Core.Infrastructure.Filters.Abstract
{
    public interface IRepoQuery
    {
        string Target { get; set; }
        Queue<string> Where { get; set; }
        Queue<string> Order { get; set; }
        int? PageSize { get; set; }
        int? Page { get; set; }
        int? Limit { get; set; }
        int? Skip { get; set; }
        int? Take { get; set; }
        IDbTransaction Transaction { get; set; }
        IEnumerable<string> Columns { get; set; }
    }
}


