using Microsoft.EntityFrameworkCore;
using MusicLife.Application.IRepositories;
using MusicLife.Application.Params;
using MusicLife.Infrastructure;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MusicApi.Infracstructure.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly DataContext _context;
        protected readonly DbSet<T> _dbSet;
        protected BaseRepository(DataContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }
        public Task<bool> ExistAsync(Expression<Func<T, bool>> expression)
        {
            return _dbSet.AsNoTracking().ApplyFilter(expression).AnyAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? expressions, params Expression<Func<T, object>>[] includes)
        {
            var query = _dbSet.AsNoTracking();
            if (expressions != null)
            {
                query.ApplyFilter(expressions);
            }
            return await query.ApplyInclude(includes).ToListAsync();
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public  Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes)
        {
            
            return _dbSet.ApplyFilter(expression)
                .ApplyInclude(includes)
                .FirstOrDefaultAsync();
        }

        public async Task<(IEnumerable<T>, int)> GetPaginationAsync(PaginationParam<T> param)
        {
            var query = _dbSet.AsNoTracking()
                .ApplyFilter(param.Expression)
                .ApplyInclude(param.Includes)
                .ApplyOrderBy(param.OrderBy);
            var totalCount=await query.CountAsync();
            query.ApplyPaginate(param.Page, param.PageSize);
            return (await query.ToListAsync(), totalCount);
        }
    }
    internal static class ExternalRepository
    {
        public static IQueryable<T> ApplyInclude<T>(this IQueryable<T> query, Expression<Func<T,object>>[] includes) where T : class
        {
            return includes == null ? query : includes.Aggregate(query, (current,include)=>current.Include(include));
        }
        public static IQueryable<T> ApplyFilter<T>(this IQueryable<T> query, Expression<Func<T,bool>> expression) 
        {
            return expression == null ? query : query.Where(expression);
        }
        public static IQueryable<T> ApplyOrderBy<T>(this IQueryable<T> query, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy)
        {
            return orderBy == null ? query : orderBy(query);
        }
        public static IQueryable<T> ApplyPaginate<T>(this IQueryable<T> query, int page, int pageSize)
        {
            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }
    }
}
