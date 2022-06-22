using System;
using System.Collections.Generic;
using System.Linq;
using Festival.DAL.Entities;

using Microsoft.EntityFrameworkCore;

namespace Festival.DAL.Repositories
{
    public class RepositoryBase<TEntity>
        where TEntity : EntityBase, new()
    {
        protected readonly IDbContextFactory<FestivalDbContext> DbContextFactory;
        public RepositoryBase(IDbContextFactory<FestivalDbContext> dbContextFactory)
        {
            DbContextFactory = dbContextFactory;
        }

        public void Delete(TEntity entity)
        {
            using var dbContext = DbContextFactory.CreateDbContext();
            dbContext.Set<TEntity>()
                 .Remove(entity);
            dbContext.SaveChanges();
        }

        public void DeleteById(Guid entityId)
        {
            var entity = new TEntity { Id = entityId };
            Delete(entity);
        }

        public TEntity GetById(Guid entityId)
        { 
            using var dbContext = DbContextFactory.CreateDbContext();
            return dbContext.Set<TEntity>()
                .SingleOrDefault(entity => entity.Id == entityId);
        }
           

        public virtual TEntity InsertOrUpdate(TEntity entity)
        {
            var dbContext = DbContextFactory.CreateDbContext();
            dbContext.Update<TEntity>(entity);
            dbContext.SaveChanges();
            return entity;
        }
        public IQueryable<TEntity> GetAll()
        {
            var dbContext = DbContextFactory.CreateDbContext();
            return dbContext.Set<TEntity>();
        }
    }
}
