using Abp.Domain.Entities;

namespace TalentMatrix.EntityFrameworkCore.Repositories
{
    public interface Repository<TEntity, TPrimaryKey> where TEntity : class, IEntity<TPrimaryKey>
    {
    }
}