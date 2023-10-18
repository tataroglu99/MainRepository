using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Linq;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Remoting.Contexts;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataAccess.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {

        private readonly DbSet<T> dbSet;
        private readonly dbShoppingEntities context;

        public GenericRepository(dbShoppingEntities dataContextFactory)
        {
            context = new dbShoppingEntities();
            dbSet = context.Set<T>();
        }

        public List<T> All(bool asNoTracking)
        {
            if (asNoTracking)
            {
                return dbSet.AsNoTracking().ToList();
            }

            else
            {
                return dbSet.ToList();
            }
        }

        public async Task<List<T>> AllAsync()
        {
            return await dbSet.ToListAsync();
        }

        public List<T> FindAll(Expression<Func<T, bool>> match)
        {
            return dbSet.Where(match).ToList();
        }

        public async Task<List<T>> FindAllAsync(Expression<Func<T, bool>> match)
        {
            return await dbSet.Where(match).ToListAsync();
        }

        public void Insert(T entity)
        {
            dbSet.Add(entity);
        }

        public void InsertAll(List<T> entity)
        {
            dbSet.AddRange(entity);
        }

        public void Update(T entity)
        {
            dbSet.AddOrUpdate(entity);
        }

        public void SoftDelete(T entity, string columnName, bool state)
        {
            entity.GetType().GetProperty(columnName)?.SetValue(entity, state);
            dbSet.AddOrUpdate(entity);
        }

        public void HardDelete(T entity)
        {
            dbSet.Remove(entity);
        }

        public T GetById<TKey>(TKey id)
        {
            return dbSet.Find(id);
        }

        public T First(Func<T, bool> exp)
        {
            return dbSet.First(exp);
        }

        public T Last(Func<T, bool> exp)
        {
            return dbSet.Last(exp);
        }

        public T Single(Expression<Func<T, bool>> match)
        {
            return dbSet.Single(match);
        }

        public async Task<T> SingleAsync(Expression<Func<T, bool>> match)
        {
            return await dbSet.SingleAsync(match);
        }

        public T SingleOrDefault(Expression<Func<T, bool>> match)
        {
            return dbSet.SingleOrDefault(match);
        }

        public async Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> match)
        {
            return await dbSet.SingleOrDefaultAsync(match);
        }

        public async Task<T> FindAsync(Expression<Func<T, bool>> match)
        {
            return await dbSet.SingleOrDefaultAsync(match);
        }

        public async Task<T> GetAsync<TKey>(TKey id)
        {
            return await dbSet.FindAsync(id);
        }

        public T Get(params object[] keyValues)
        {
            return dbSet.Find(keyValues);
        }

        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return dbSet.Where(predicate);
        }

        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate, string include)
        {
            return FindBy(predicate).Include(include);
        }

        public ICollection<T> FindAllCollection(Expression<Func<T, bool>> match)
        {
            return dbSet.Where(match).ToList();
        }

        public List<T> GetAllWithExpression(Expression<Func<T, bool>> predicate, bool asNoTracking)
        {
            return dbSet.Where(predicate).AsNoTracking().ToList();
        }

        public List<T> GetMultiple(bool asNoTracking)
        {
            return FindQueryable(asNoTracking).ToList();
        }

        public async Task<List<T>> GetMultipleAsync(bool asNoTracking, Expression<Func<T, bool>> whereExpression, CancellationToken cancellationToken = default)
        {
            return await FindQueryable(asNoTracking).Where(whereExpression).ToListAsync(cancellationToken).ConfigureAwait(false);
        }

        public IQueryable<T> GetAll(bool asNoTracking)
        {
            if (asNoTracking)
            {
                return dbSet.AsNoTracking();
            }

            else
            {
                return dbSet;
            }
        }

        public IQueryable<T> GetAll(int page, int pageCount)
        {
            var pageSize = (page - 1) * pageCount;

            return dbSet.Skip(pageSize).Take(pageCount);
        }

        public IQueryable<T> GetAll(string include)
        {
            return dbSet.Include(include);
        }

        public IQueryable<T> GetAll(string include, string include2)
        {
            return dbSet.Include(include).Include(include2);
        }

        public bool Exists(Expression<Func<T, bool>> predicate)
        {
            return dbSet.Any(predicate);
        }

        public int Count()
        {
            return dbSet.Count();
        }

        public int Count(Expression<Func<T, bool>> whereExpression)
        {
            return dbSet.Where(whereExpression).Count();
        }

        public async Task<int> Count(Expression<Func<T, bool>> whereExpression, CancellationToken cancellationToken = default)
        {
            int count = await dbSet.Where(whereExpression).CountAsync(cancellationToken).ConfigureAwait(false);
            return count;
        }

        public async Task<int> CountAsync(CancellationToken cancellationToken = default)
        {
            int count = await dbSet.CountAsync(cancellationToken).ConfigureAwait(false);
            return count;
        }

        private IQueryable<T> FindQueryable(bool asNoTracking)
        {
            var queryable = GetQueryable();

            if (asNoTracking)
            {
                queryable = queryable.AsNoTracking();
            }
            return queryable;
        }

        public IQueryable<T> GetQueryable()
        {
            return dbSet.AsQueryable();
        }

        public bool Commit()
        {
            bool state = false;
            var transaction = context.Database.BeginTransaction();

            try
            {
                context.SaveChanges();
                transaction.Commit();
                state = true;
            }

            catch (Exception ex)
            {
                transaction.Rollback();
            }

            return state;
        }

        public async Task<bool> CommitAsync()
        {
            bool state = false;
            var transaction = context.Database.BeginTransaction();

            try
            {
                await context.SaveChangesAsync(CancellationToken.None);
                transaction.Commit();
                state = true;
            }

            catch (Exception ex)
            {
                transaction.Rollback();
            }

            return state;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(obj: this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (context != null)
                {
                    context.Dispose();
                }
            }
        }

        public void GetLogValue(object tempObj, ref string newValueLog)
        {
            string sline = tempObj.GetType().Name.ToUpper() + " ";

            newValueLog = string.Format("{0} Table\n", tempObj.GetType().BaseType.Name);//tempObj.GetType().BaseType.Name

            List<MemberInfo> values = dbSet.GetType().GetMembers().ToList<MemberInfo>();
            List<PropertyInfo> properties = tempObj.GetType().GetProperties().ToList<PropertyInfo>();

            foreach (PropertyInfo info in tempObj.GetType().GetProperties())
            {
                if (info.CanRead)
                {
                    if (info.PropertyType.FullName.Contains("System") && !info.PropertyType.FullName.Contains(".Linq"))
                    {
                        if (values != null && values.Count > 0)
                        {
                            if (values.Exists(exp => exp.Name.Equals(info.Name)))
                            {
                                MemberInfo eskideger = values.SingleOrDefault(exp => exp.Name.Equals(info.Name));

                                newValueLog += string.Format("{0}: {1}\n", info.Name, DateSettings(eskideger.Name));
                            }

                            else
                            {
                                newValueLog += string.Format("{0}: {1}\n", info.Name, DateSettings(info.GetValue(tempObj, null)));
                            }
                        }

                        else
                        {
                            newValueLog += string.Format("{0}: {1}\n", info.Name, DateSettings(info.GetValue(tempObj, null)));
                        }
                    }
                }
            }
        }

        private object DateSettings(object value)
        {
            if (value != null)
            {
                return value.GetType() == Type.GetType("System.DateTime") ? ((DateTime)value).ToString("dd/MM/yyyy HH:mm:ss") : value;
            }
            else
            {
                return value;
            }
        }
    }
}
