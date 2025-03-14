using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MusicLife.Domain.Commons
{
    public class PaginationParam<T> where T : class
    {
        public int Page {  get; set; }
        public int PageSize { get; set; }
        public Expression<Func<T, bool>>[] Expressions { get; set; } = null!;
        public Expression<Func<T, object>>[] Includes { get; set; } = null!;
        public Func<IQueryable<T>, IOrderedQueryable<T>> OrderBy { get; set; } = null!;

    }
}
