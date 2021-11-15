using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Core.Dal.Abstract
{
    public interface IEntityRepository<TEntity> where TEntity : class, IEntity, new()
    {
        TEntity Add(TEntity entity);
        TEntity Update(TEntity entity);
        TEntity Get(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, IEntity>>[] includes = null);
        TEntity GetById(int id);
        void Delete(TEntity entity);
        List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null, Expression<Func<TEntity, object>>[] includes = null);
    }
}
