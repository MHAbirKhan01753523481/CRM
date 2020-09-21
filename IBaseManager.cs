using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProjectMaking.BaseManager
{
   public interface IBaseManager<TEntity>where TEntity : class
    {

        void Add(TEntity entity);
        int Count(Expression<Func<TEntity, bool>> predicate);
        void Remove(TEntity entity);
        void RemoveRange(TEntity entity);
        void Update(TEntity entity);
        TEntity Find(int id);
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>> predicate);
        TEntity GetFirstOrDefault();
        TEntity Get(int id);
        TEntity Get(long id);
        IQueryable<TEntity> AsQueryable();
        List<TEntity> GetAll();

        
    }
}
