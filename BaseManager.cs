using Microsoft.EntityFrameworkCore;
using ProjectMaking.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProjectMaking.BaseManager
{
    public class BaseManager<TEntity> :IBaseManager <TEntity> where TEntity : class
    {
        protected readonly DbSet<TEntity> _entities;

        protected readonly AppDbContext context;
        public BaseManager(AppDbContext _context)
        {
            this.context = _context;
            _entities = context.Set<TEntity>();
        }

        public virtual void Add(TEntity entity)
        {
            try
            {
                _entities.Add(entity);
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "-- " + ex.InnerException);
            }
        }
        public virtual void AddRange(TEntity entity)
        {
            try
            {
                _entities.AddRange(entity);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message+"-- "+ex.InnerException);
            }
           
        }

        public virtual void Update(TEntity entity)
        {
            _entities.Update(entity);      
        }
        public virtual void UpdateRange(TEntity entity)
        {
            _entities.UpdateRange(entity);
        }
        public virtual int Count(TEntity entity)
        {
            return _entities.Count();
        }

        public virtual int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return _entities.Count();
        }

        public virtual void Remove(TEntity entity)
        {
            _entities.Remove(entity);
        }

        public virtual void RemoveRange(TEntity entity)
        {
            _entities.RemoveRange(entity);
        }

        public virtual TEntity Find(int id)
        {
            return _entities.Find(id);
        }

        public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _entities.Where(predicate);
        }

        public virtual TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return _entities.FirstOrDefault(predicate);
        }

        public virtual TEntity GetFirstOrDefault()
        {
           return _entities.FirstOrDefault();
        }

        public TEntity Get(int id)
        {
            return _entities.Find(id);
        }

        public TEntity Get(long id)
        {
            return _entities.Find(id);
        }

        public IQueryable<TEntity> AsQueryable()
        {
            var t = typeof(TEntity);
            var isDeleted = t.GetProperty("IsDelete");
            return _entities.Where(x => (bool)isDeleted.GetValue(x, null) == false);
        }

        public List<TEntity> GetAll()
        {
            return _entities.ToList();
        }
    }
}
