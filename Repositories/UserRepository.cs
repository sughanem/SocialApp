// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Identity;
// using Microsoft.EntityFrameworkCore.ChangeTracking;
// using SocialApp.Shared;

// namespace SocialAppService.Repositories
// {
//     public class UserRepository
//     {

//         private readonly CacheRepository<User> cacheRepo;
//         private readonly SocialAppDatabase dbContext;
//         private readonly UserManager<User> userManager;
            
//         public UserRepository(SocialAppDatabase dbContext, CacheRepository<User> cacheRepo,
//             UserManager<User> userManager)
//         {
//             this.dbContext = dbContext;
//             this.cacheRepo = cacheRepo;
//             this.userManager = userManager;
//         }
//         public async Task<IdentityResult> CreateAsync(User user)
//         {
            
//             return await userManager.CreateAsync(user, user.PasswordHash);
//         }
//         public async Task<TEntity> RetrieveAsync(int id)
//         {
//             return await cacheRepo.GetFromCache(id,
//                 async () => {
//                     TEntity instance = await dbContext.Set<TEntity>().FindAsync(id);
//                     return instance;
//                 });
//         }
//         public Task<IEnumerable<TEntity>> RetrieveAllAsync()
//         {
//             return Task.Run<IEnumerable<TEntity>>( () => dbContext.Set<TEntity>().ToList() );
            
//         }
//         public async Task<TEntity> UpdateAsync(int id, TEntity instance)
//         {
//             instance.Id = id;
//             dbContext.Set<TEntity>().Update(instance);
//             int affected = await dbContext.SaveChangesAsync();
//             if (affected == 1)
//             {
//                 await cacheRepo.SetCache(Convert.ToString(id), instance);
//                 return instance;
//             }
//             return null;
//         }

//         public async Task<bool?> DeleteAsync(int id)
//         {
//             var instance = dbContext.Set<TEntity>().Find(id);
//             dbContext.Set<TEntity>().Remove(instance);
//             int affected = await dbContext.SaveChangesAsync();
//             if (affected == 1)
//             {
//                 await cacheRepo.ClearCache(Convert.ToString(id));
//                 return true;
//             }
//             return false;
//         }
//         public bool ifExisting(int id) => dbContext.Set<TEntity>().Any(instance => instance.Id == id);

//     }
// }