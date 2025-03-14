using MusicLife.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MusicLife.Application.IRepositories
{
    public interface IBaseRepository<T> where T : class
    {
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
        Task<List<T>> GetAllAsync(List<Expression<Func<T, bool>>>? expressions = null, params Expression<Func<T, object>>[] includes);
        Task<(List<T>, int)> GetPaginationAsync(PaginationParam<T> param);
        Task<bool> ExistAsync(Expression<Func<T, bool>>[] expression);
        Task<T?> GetByIdAsync(Guid id);
        Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>>[] expressions, params Expression<Func<T, object>>[] includes);
       
    }
}
