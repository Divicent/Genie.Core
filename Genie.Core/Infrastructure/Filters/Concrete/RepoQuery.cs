using System.Collections.Generic;
using System.Data;
using Genie.Core.Infrastructure.Filters.Abstract;

namespace Genie.Core.Infrastructure.Filters.Concrete
{
    public class RepoQuery : IRepoQuery
    {
        internal RepoQuery()
        {
        }

        public string Target { get; set; }
        public Queue<string> Where { get; set; }
        public Queue<string> Order { get; set; }
        public int? PageSize { get; set; }
        public int? Page { get; set; }
        public int? Limit { get; set; }
        public int? Skip { get; set; }
        public int? Take { get; set; }
        public IDbTransaction Transaction { get; set; }
        public IEnumerable<string> Columns { get; set; }
    }

}

