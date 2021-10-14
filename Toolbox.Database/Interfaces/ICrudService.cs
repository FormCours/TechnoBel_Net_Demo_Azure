using System.Collections.Generic;

namespace Toolbox.Database.Interfaces
{
    public interface ICrudService<TKey, TEntity>
        where TEntity : class, IEntity<TKey>
    {
        TEntity GetById(TKey id);
        IEnumerable<TEntity> GetAll();

        TKey Insert(TEntity entity);
        bool Update(TKey id, TEntity entity);
        bool Delete(TKey id);
    }
}
