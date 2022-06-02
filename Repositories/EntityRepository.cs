using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SocialApp.Database;

namespace SocialAppService.Repositories
{
    public class EntityRepository<TEntity> : IEntityRepository<TEntity> 
    where TEntity : class, EntityBase
    {

        private readonly CacheRepository<TEntity> cacheRepo;
        private readonly SocialAppDatabase dbContext;
            
        public EntityRepository(SocialAppDatabase dbContext, CacheRepository<TEntity> cacheRepo)
        {
            this.dbContext = dbContext;
            this.cacheRepo = cacheRepo;
        }
        public async Task<TEntity> CreateAsync(TEntity instance)
        {
            return await cacheRepo.GetFromCache(instance.Id,
            async () => {
                var added = await dbContext.Set<TEntity>().AddAsync(instance);
                await dbContext.SaveChangesAsync();
                return added.Entity;
            });
        }
        public async Task<TEntity> RetrieveAsync(int id)
        {
            return await cacheRepo.GetFromCache(id,
            async () => {
                TEntity instance = await dbContext.Set<TEntity>().FindAsync(id);
                return instance;
            });
        }
        public Task<IEnumerable<TEntity>> RetrieveAllAsync()
        {
            return Task.Run<IEnumerable<TEntity>>( () => dbContext.Set<TEntity>().ToList() );
            
        }
        public async Task<TEntity> UpdateAsync(int id, TEntity instance)
        {
            // instance.Id = id;
            dbContext.Set<TEntity>().Update(instance);
            int affected = await dbContext.SaveChangesAsync();
            if (affected == 1)
            {
                await cacheRepo.SetCache(Convert.ToString(id), instance);
                return instance;
            }
            return null;
        }

        public async Task<bool?> DeleteAsync(int id)
        {
            var instance = dbContext.Set<TEntity>().Find(id);
            dbContext.Set<TEntity>().Remove(instance);
            int affected = await dbContext.SaveChangesAsync();
            if (affected == 1)
            {
                await cacheRepo.ClearCache(Convert.ToString(id));
                return true;
            }
            return false;
        }
        public bool ifExisting(int id) => dbContext.Set<TEntity>().Any(instance => instance.Id == id);

        public SocialAppDatabase getDbContext()
        {
            return this.dbContext;
        }
    }
}