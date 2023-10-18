using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        List<T> All(bool asNoTracking);
        Task<List<T>> AllAsync();
        bool Commit();
        Task<bool> CommitAsync();
        int Count();
        int Count(Expression<Func<T, bool>> whereExpression);
        Task<int> Count(Expression<Func<T, bool>> whereExpression, CancellationToken cancellationToken = default);
        Task<int> CountAsync(CancellationToken cancellationToken = default);
        void Dispose();
        bool Exists(Expression<Func<T, bool>> predicate);
        List<T> FindAll(Expression<Func<T, bool>> match);
        Task<List<T>> FindAllAsync(Expression<Func<T, bool>> match);
        ICollection<T> FindAllCollection(Expression<Func<T, bool>> match);
        Task<T> FindAsync(Expression<Func<T, bool>> match);
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate, string include);
        T First(Func<T, bool> exp);
        T Get(params object[] keyValues);
        IQueryable<T> GetAll(bool asNoTracking);
        IQueryable<T> GetAll(int page, int pageCount);
        IQueryable<T> GetAll(string include);
        IQueryable<T> GetAll(string include, string include2);
        List<T> GetAllWithExpression(Expression<Func<T, bool>> predicate, bool asNoTracking);
        Task<T> GetAsync<TKey>(TKey id);
        T GetById<TKey>(TKey id);
        void GetLogValue(object tempObj, ref string newValueLog);
        List<T> GetMultiple(bool asNoTracking);
        Task<List<T>> GetMultipleAsync(bool asNoTracking, Expression<Func<T, bool>> whereExpression, CancellationToken cancellationToken = default);
        IQueryable<T> GetQueryable();
        void HardDelete(T entity);
        void Insert(T entity);
        void InsertAll(List<T> entity);
        T Last(Func<T, bool> exp);
        T Single(Expression<Func<T, bool>> match);
        Task<T> SingleAsync(Expression<Func<T, bool>> match);
        T SingleOrDefault(Expression<Func<T, bool>> match);
        Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> match);
        void SoftDelete(T entity, string columnName, bool state);
        void Update(T entity);
    }
}