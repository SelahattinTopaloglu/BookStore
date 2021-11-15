using Core.Dal.Abstract;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Core.Dal.Concrete
{
    public class EntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
        where TContext : DbContext, new()
    {
        public TEntity Add(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var addedEntity = context.Entry(entity);
                addedEntity.State = EntityState.Added;
                context.SaveChanges();
                return addedEntity.Entity;
            }
        }

        public void Delete(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, IEntity>>[] includes = null)
        {
            using (TContext context = new TContext())
            {
                var queryable = context.Set<TEntity>();
                if (includes != null)
                {
                    foreach (var item in includes)
                    {
                        queryable.Include(item);
                    }
                }
                return queryable.SingleOrDefault(filter);
            }
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null, Expression<Func<TEntity, object>>[] includes = null)
        {
            using (TContext context = new TContext())
            {
                var queryable = context.Set<TEntity>();
                if (includes != null)
                {
                    foreach (var item in includes)
                    {
                        queryable.Include(item);
                    }
                }
                return filter == null ? queryable.ToList()
                                      : queryable.Where(filter).ToList();
            }
        }

        public TEntity GetById(int id)
        {
            using (TContext context = new TContext())
            {
                return context.Set<TEntity>().SingleOrDefault(e => e.Id == id);
            }
        }

        public TEntity Update(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges();
                return updatedEntity.Entity;
            }
        }
    }
}
