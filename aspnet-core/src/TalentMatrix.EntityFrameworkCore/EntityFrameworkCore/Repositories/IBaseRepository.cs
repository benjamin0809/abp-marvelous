using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace TalentMatrix.EntityFrameworkCore.Repositories
{
    public abstract class IBaseRepository<TEntity, TPrimaryKey> : Repository<TEntity, TPrimaryKey> where TEntity : class, IEntity<TPrimaryKey>
    {
        public abstract void BulkDelete(IList<TEntity> entities);
        public abstract void BulkInsert(IList<TEntity> entities);
    }
}
