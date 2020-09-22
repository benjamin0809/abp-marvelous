using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TalentMatrix.EntityFrameworkCore.Repositories
{
    public abstract class BaseRepository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>
    {
        protected BaseRepository()
        {
        }

        public abstract int Count();
        public abstract int Count(Expression<Func<TEntity, bool>> predicate);
        public abstract Task<int> CountAsync();
        public abstract Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate);
        public abstract void Delete(TEntity entity);
        public abstract void Delete(TKey id);
        public abstract void Delete(Expression<Func<TEntity, bool>> predicate);
        public abstract Task DeleteAsync(TEntity entity);
        public abstract Task DeleteAsync(TKey id);
        public abstract Task DeleteAsync(Expression<Func<TEntity, bool>> predicate);
        public abstract TEntity FirstOrDefault(TKey id);
        public abstract TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);
        public abstract Task<TEntity> FirstOrDefaultAsync(TKey id);
        public abstract Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        public abstract TEntity Get(TKey id);
        public abstract IQueryable<TEntity> GetAll();
        public abstract IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] propertySelectors);
        public abstract List<TEntity> GetAllList();
        public abstract List<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate);
        public abstract Task<List<TEntity>> GetAllListAsync();
        public abstract Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate);
        public abstract Task<TEntity> GetAsync(TKey id);
        public abstract TEntity Insert(TEntity entity);
        public abstract TKey InsertAndGetId(TEntity entity);
        public abstract Task<TKey> InsertAndGetIdAsync(TEntity entity);
        public abstract Task<TEntity> InsertAsync(TEntity entity);
        public abstract TEntity InsertOrUpdate(TEntity entity);
        public abstract TKey InsertOrUpdateAndGetId(TEntity entity);
        public abstract Task<TKey> InsertOrUpdateAndGetIdAsync(TEntity entity);
        public abstract Task<TEntity> InsertOrUpdateAsync(TEntity entity);
        public abstract TEntity Load(TKey id);
        public abstract long LongCount();
        public abstract long LongCount(Expression<Func<TEntity, bool>> predicate);
        public abstract Task<long> LongCountAsync();
        public abstract Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate);
        public abstract T Query<T>(Func<IQueryable<TEntity>, T> queryMethod);
        public abstract TEntity Single(Expression<Func<TEntity, bool>> predicate);
        public abstract Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate);
        public abstract TEntity Update(TEntity entity);
        public abstract TEntity Update(TKey id, Action<TEntity> updateAction);
        public abstract Task<TEntity> UpdateAsync(TEntity entity);
        public abstract Task<TEntity> UpdateAsync(TKey id, Func<TEntity, Task> updateAction);

        public abstract void BulkInsert<T>(List<T> entities);
        public abstract void BulkDelete<T>(List<T> entities);
    }
}
